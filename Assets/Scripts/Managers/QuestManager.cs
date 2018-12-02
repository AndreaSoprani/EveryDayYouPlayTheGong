using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Quests;
using Quests.Objectives;
using UnityEditor;
using UnityEngine;
using UnityScript.Scripting.Pipeline;
using Utility;

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

	public Settings Settings;
	
	public List<Quest> Quests = new List<Quest>();
	public List<Quest> TodayQuests = new List<Quest>();
	public List<Quest> QuestsOnHold = new List<Quest>();
	
	
	/// <summary>
	/// Starts listening on all active quests.
	/// </summary>
	private void Start()
	{
		for (int i = 0; i < Quests.Count; i++)
		{
			if(TodayQuests.Contains(Quests[i])) ListenOnObjectives(Quests[i]);
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
	/// Used to activate a quest.
	/// </summary>
	/// <param name="quest">The quest to activate.</param>
	public void ActivateQuest(Quest quest)
	{
		if (quest == null) return;

		if (IsDayFull())
		{
			QuestsOnHold.Add(quest);
			return;
		}
		
		TodayQuests.Add(quest);
		
		GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyQuestActivated", quest);
		ListenOnObjectives(quest);
	}

	/// <summary>
	/// Used by quests upon completion.
	/// </summary>
	/// <param name="quest">The completed quest.</param>
	public void QuestCompleted(Quest quest)
	{
		
		if (quest == null || !quest.IsComplete()) return;
		
		DayProgressionManager.Instance.Progress();
		
		GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyCompletedQuest", quest);
		
		// Activate all quests to activate.
		for (int i = 0; i < quest.ActivateWhenComplete.Count; i++)
		{
			ActivateQuest(quest.ActivateWhenComplete[i]);
		}
		
		// Stop listening on objectives.
		StopListeningOnObjectives(quest);

	}

	/// <summary>
	/// Used to activate quests on hold until the day schedule is full or there are no quests on hold.
	/// </summary>
	public void PopQuestsOnHold()
	{
		while (!IsDayFull() && QuestsOnHold.Count > 0)
		{
			Quest quest = QuestsOnHold[0];
			QuestsOnHold.Remove(quest);
			ActivateQuest(quest);
		}
	}

	/// <summary>
	/// Used to check if the "day schedule" is full.
	/// </summary>
	/// <returns>True if the schedule is full, false otherwise.</returns>
	public bool IsDayFull()
	{
		if (TodayQuests.Count == Settings.QuestsPerDay) return true;
		return false;
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
		}
	}
	
}
