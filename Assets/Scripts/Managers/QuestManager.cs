using System.Collections;
using System.Collections.Generic;
using Quests;
using Quests.Objectives;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class QuestManager : MonoBehaviour
{
	
	private static QuestManager _instance;
	public static QuestManager Instance
	{
		get { return _instance;  }
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	public List<Quest> Quests;
	
	// Use this for initialization
	void Start ()
	{
		Quests = new List<Quest>();
	}

	public void AddQuest(Quest quest)
	{
		Quests.Add(quest);
		
		// Listen on events for all objectives.
		Objective[] objectives = quest.Objectives;
		for (int i = 0; i < objectives.Length; i++)
		{
			objectives[i].StartListening();
		}
	}

	public void RemoveQuest(Quest quest)
	{
		Quests.Remove(quest);
		
		// Stop listening on events for all objectives.
		Objective[] objectives = quest.Objectives;
		for (int i = 0; i < objectives.Length; i++)
		{
			objectives[i].StopListening();
		}
	}

	
	
}
