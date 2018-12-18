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
	public List<string> SoundName;
	public bool HasInteraction;
	public float PauseTime;
	
	/*
	 * PLAY
	 */

	public override bool IsPlayable()
	{
		return true;
	}

	public override void Play()
	{
		StartCoroutine(DelayPlay());
	}

	private IEnumerator DelayPlay()
	{
		yield return new WaitForSeconds(0.12f);
		Animator animator = GetComponent<Animator>();
		if (animator != null) animator.SetTrigger("Playing");
		foreach (string s in SoundName)
		{
				AudioManager.Instance.PlayEvent(s);
				yield return new WaitForSeconds(PauseTime);
		}
		
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
