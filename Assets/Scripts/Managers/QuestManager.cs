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

	/// <summary>
	/// Adds a quest to the list of quests in the manager.
	/// It also makes all the objectives listen for events for their completeness.
	/// </summary>
	/// <param name="quest">The quest to add.</param>
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
	
	/// <summary>
	/// Removes a quest from the list of quests in the manager.
	/// It also makes all the objectives stop listening for events for their completeness.
	/// </summary>
	/// <param name="quest">The quest to remove.</param>
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

	/// <summary>
	/// Used to get a quest by ID.
	/// </summary>
	/// <param name="questID">The QuestID of the quest to retrieve.</param>
	/// <returns></returns>
	public static Quest GetQuest(string questID)
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			if (Quests[i].QuestID == questID) return Quests[i];
		}

		return null;
	}

	/// <summary>
	/// Used to activate a quest given its ID.
	/// </summary>
	/// <param name="questID">The QuestID of the quest to activate.</param>
	public static void ActivateQuest(string questID)
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			if (Quests[i].QuestID == questID) Quests[i].Active = true;
		}
	}

	/// <summary>
	/// Used to deactivate a quest given its ID.
	/// </summary>
	/// <param name="questID">The QuestID of the quest to deactivate.</param>
	public static void DeactivateQuest(string questID)
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			if (Quests[i].QuestID == questID) Quests[i].Active = false;
		}
	}
	
}
