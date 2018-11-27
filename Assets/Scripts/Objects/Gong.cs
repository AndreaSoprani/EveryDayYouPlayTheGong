using System.Collections;
using System.Collections.Generic;
using Objects;
using Quests;
using Quests.Objectives;
using UnityEditor;
using UnityEngine;
using Utility;

public class Gong : InGameObject
{

	public Dialogue Dialogue;
	public string SoundName;

	/*
	 * PLAY
	 */

	public override bool IsPlayable()
	{
		return true;
	}

	public override void Play()
	{
		AudioManager.Instance.PlaySound(SoundName);
	}

	
	/*
	 * INTERACT
	 */


	public override void Interact(Player player)
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
		iObj.ObjectID = this.ObjectID;
		
		PlayObjective pObj = ScriptableObject.CreateInstance<PlayObjective>();
		pObj.ObjectiveID = "O_01";
		pObj.QuestID = quest.QuestID;
		pObj.Description = "Test play objective";
		pObj.Completed = false;
		pObj.ObjectID = this.ObjectID;
		
		quest.Objectives.Add(iObj);
		quest.Objectives.Add(pObj);
		
		QuestManager.Instance.AddQuest(quest);
	}
}
