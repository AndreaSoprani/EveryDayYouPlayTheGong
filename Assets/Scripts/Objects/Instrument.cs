using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class Instrument : InGameObject
{

	public Dialogue Dialogue;
	public List<string> SoundName;
	public float AnimationDelay = 0.12f;
	public float SoundDelay = 0.12f;
	public bool HasInteraction;
	public float PauseTime;

	private Animator _animator;
	
	private void Start()
	{
		_animator = GetComponent<Animator>();
	}
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
		StartCoroutine(DelayAnimation());
	}

	private IEnumerator DelayPlay()
	{
		yield return new WaitForSeconds(SoundDelay);
		foreach (string s in SoundName)
		{
				AudioManager.Instance.PlayEvent(s);
				yield return new WaitForSeconds(PauseTime);
		}
	}

	private IEnumerator DelayAnimation()
	{
		yield return new WaitForSeconds(AnimationDelay);
		if (_animator != null) _animator.SetTrigger("Playing");
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
