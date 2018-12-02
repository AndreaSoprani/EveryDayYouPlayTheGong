using System.Collections;
using System.Collections.Generic;
using Objects;
using Quests;
using Quests.Objectives;
using UnityEditor;
using UnityEngine;
using Utility;

public class Instrument : InGameObject
{

	public Dialogue Dialogue;
	public Sound Sound;

	/*
	 * PLAY
	 */

	public override bool IsPlayable()
	{
		return true;
	}

	public override void Play()
	{
		AudioManager.Instance.PlaySound(Sound);
	}

	
	/*
	 * INTERACT
	 */


	public override void Interact()
	{
		Dialogue.StartDialogue();
	}
	
	
}
