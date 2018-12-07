using System;
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
	public string SoundName;
	public bool HasInteraction;
	
	/*
	 * PLAY
	 */

	public override bool IsPlayable()
	{
		return true;
	}

	public override void Play()
	{
		StartCoroutine(DelayPlayAnimation());


		AkSoundEngine.PostEvent(SoundName, gameObject);
	}

	private IEnumerator DelayPlayAnimation()
	{
		yield return new WaitForSeconds(0.23f);
		GetComponent<Animator>().SetTrigger("Playing");
	}
	/*
	 * INTERACT
	 */

	public override bool IsInteractable()
	{
		return HasInteraction;
	}

	public override void Interact()
	{
		Dialogue.StartDialogue();
	}
	
	
}
