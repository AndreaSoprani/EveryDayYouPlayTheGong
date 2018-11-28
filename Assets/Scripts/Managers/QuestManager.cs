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

	public List<Quest> Quests = new List<Quest>();
	

	
	/// <summary>
	/// Starts listening on all active quests.
	/// </summary>
	private void Start()
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			if(Quests[i].Active) ListenOnObjectives(Quests[i]);
		}
	}

	/// <summary>
	/// Adds a quest to the list of quests in the manager.
	/// It also makes all the objectives listen for events for their completeness.
	/// </summary>
	/// <param name="quest">The quest to add.</param>
	public void AddQuest(Quest quest)
	{
		Quests.Add(quest);
		// Listen on events for all objectives.
		ListenOnObjectives(quest);
	}

	/// <summary>
	/// Used to get a quest by ID.
	/// </summary>
	/// <param name="questID">The QuestID of the quest to retrieve.</param>
	/// <returns></returns>
	public Quest GetQuest(string questID)
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
	public void ActivateQuest(string questID)
	{
		Quest quest = GetQuest(questID);

		if (quest == null) return;
		
		quest.Active = true;
		ListenOnObjectives(quest);
	}

	/// <summary>
	/// Used to deactivate a quest given its ID.
	/// </summary>
	/// <param name="questID">The QuestID of the quest to deactivate.</param>
	public void DeactivateQuest(string questID)
	{
		Quest quest = GetQuest(questID);

		if (quest == null) return;
		
		quest.Active = false;
		StopListeningOnObjectives(quest);
	}

	/// <summary>
	/// Used by objectives to check if the quest is completed and de-activate it (and activate possible quests).
	/// </summary>
	/// <param name="questID">The id of the quest.</param>
	public void CheckComplete(string questID)
	{
		Quest quest = GetQuest(questID);
		
		if (quest == null || !quest.IsComplete()) return;
		
		Debug.Log("Quest " + questID + " Completed");

		// Activate all quests to activate.
		for (int i = 0; i < quest.ActivateWhenComplete.Count; i++)
		{
			ActivateQuest(quest.ActivateWhenComplete[i].QuestID);
		}
		
		// Deactivate completed quest.
		DeactivateQuest(questID);

	}

	/// <summary>
	/// Used to listen for the completeness event on all objectives of the quest.
	/// </summary>
	/// <param name="quest"></param>
	private void ListenOnObjectives(Quest quest)
	{
		List<Objective> objectives = quest.Objectives;
		for (int i = 0; i < objectives.Count; i++)
		{
			objectives[i].StartListening();
		}
	}

	/// <summary>
	/// Used to stop listening for the completeness event on all objectives of the quest.
	/// </summary>
	/// <param name="quest"></param>
	private void StopListeningOnObjectives(Quest quest)
	{
		List<Objective> objectives = quest.Objectives;
		for (int i = 0; i < objectives.Count; i++)
		{
			objectives[i].StopListening();
		}
	}

	/// <summary>
	/// On destroy "de-complete" all objectives and deactivates all quests.
	/// </summary>
	private void OnDestroy()
	{
		foreach (Quest quest in Quests)
		{
			foreach (Objective objective in quest.Objectives)
			{
				objective.Completed = false;
			}

			quest.Active = false;
		}
	}
	
}
