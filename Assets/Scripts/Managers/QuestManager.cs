using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	public static List<Quest> Quests = new List<Quest>();

	public static void AddQuest(Quest quest)
	{
		Quests.Add(quest);
		
		// Listen on events for all objectives.
		Objective[] objectives = quest.Objectives.ToArray();
		for (int i = 0; i < objectives.Length; i++)
		{
			objectives[i].StartListening();
		}
	}

	public static void RemoveQuest(Quest quest)
	{
		Quests.Remove(quest);
		
		// Stop listening on events for all objectives.
		Objective[] objectives = quest.Objectives.ToArray();
		for (int i = 0; i < objectives.Length; i++)
		{
			objectives[i].StopListening();
		}
	}

	public static Quest GetQuestById(string questID)
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			Debug.Log("for loop: quest " + Quests[i].QuestID);
			if (Quests[i].QuestID == questID) return Quests[i];
		}

		return null;
	}
	
}
