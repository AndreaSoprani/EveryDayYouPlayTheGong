using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

/// <summary>
/// At startup loads all quests in the QuestManager.
/// </summary>
public class QuestLoader : MonoBehaviour
{
	
	public List<Quest> Quests;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < Quests.Count; i++)
		{
			QuestManager.AddQuest(Quests[i]);
		}
	}
}
