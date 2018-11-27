using System.Collections;
using System.Collections.Generic;
using Quests;
using Quests.Objectives;
using UnityEditor;
using UnityEngine;
using Utility;

public class Gong : MonoBehaviour, IPlayableObject, IInteractiveObject
{

	public Dialogue Dialogue;

	/*
	 * PLAY
	 */

	public bool IsPlayable()
	{
		return true;
	}

	public void Play()
	{
		AudioManager.Instance.PlaySound("GongSoundTest");
	}

	
	/*
	 * INTERACT
	 */
	
	public bool IsInteractable()
	{
		return true;
	}

	public void Interact(Player player)
	{
		Dialogue.StartDialogue();
	}
	
	/*
	 * FOR TESTING PURPOSES
	 */

	private void Start()
	{
		Quest quest = ScriptableObject.CreateInstance<Quest>();
		quest.QuestID = "00";
		quest.Description = "Test play quest";
		quest.Objectives = new List<Objective>();
		

		InteractObjective iObj = ScriptableObject.CreateInstance<InteractObjective>();
		iObj.ObjectiveID = "O_00";
		iObj.QuestID = quest.QuestID;
		iObj.Description = "Test interact objective";
		iObj.Completed = false;
		iObj.InteractiveObject = GetComponent<IInteractiveObject>();
		
		PlayObjective pObj = ScriptableObject.CreateInstance<PlayObjective>();
		pObj.ObjectiveID = "O_01";
		pObj.QuestID = quest.QuestID;
		pObj.Description = "Test play objective";
		pObj.Completed = false;
		pObj.PlayableObject = GetComponent<IPlayableObject>();
		
		quest.Objectives.Add(iObj);
		quest.Objectives.Add(pObj);
		
		QuestManager.Instance.AddQuest(quest);
	}
}
