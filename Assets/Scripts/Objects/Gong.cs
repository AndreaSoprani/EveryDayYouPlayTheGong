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
		PlayObjective obj = new PlayObjective
         		{
			ObjectiveID = "O_00", 
			Description = "Test play objective", 
			Completed = false, 
			PlayableObject = GetComponent<IPlayableObject>()
		};
		
		Quest quest = new Quest
		{
			QuestID = "00", 
			Description = "Test play quest", 
			Objectives = new List<Objective> {obj}
		};
		
		QuestManager.AddQuest(quest);
	}
}
