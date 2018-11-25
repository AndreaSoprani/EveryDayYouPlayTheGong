using System.Collections;
using System.Collections.Generic;
using Quests;
using Quests.Objectives;
using UnityEditor;
using UnityEngine;

public class Gong : MonoBehaviour, IPlayableObject, IInteractiveObject
{

	public TextAsset InteractionMessage;
	public string GongName;

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
		TextBoxManager.Instance.LoadScript(InteractionMessage, GongName);
		TextBoxManager.Instance.EnableTextBox();
	}
	
	/*
	 * FOR TESTING PURPOSES
	 */

	private void Start()
	{
		Quest quest = new Quest
		{
			QuestID = "00", 
			Description = "Test play quest", 
			Objectives = new List<Objective>()
		};

		InteractObjective iObj = new InteractObjective
		{
			ObjectiveID = "O_00", 
			Quest = quest,
			Description = "Test interact objective", 
			Completed = false, 
			InteractiveObject = GetComponent<IInteractiveObject>()
		};
		
		PlayObjective pObj = new PlayObjective
         {
			ObjectiveID = "O_01", 
			Quest = quest,
			Description = "Test play objective", 
			Completed = false, 
			PlayableObject = GetComponent<IPlayableObject>()
		};
		
		quest.Objectives.Add(iObj);
		quest.Objectives.Add(pObj);
		
		QuestManager.AddQuest(quest);
	}
}
