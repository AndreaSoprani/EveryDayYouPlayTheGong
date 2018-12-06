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
	public Sound Sound;
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
		
		//TODO Check if this call has to be delayed too
		AudioManager.Instance.PlaySound(Sound);
	}

	private IEnumerator DelayPlayAnimation()
	{
		yield return new WaitForSeconds(0.23f);
		Animator animator = GetComponent<Animator>();
		if(animator!=null) animator.SetTrigger("Playing");
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
