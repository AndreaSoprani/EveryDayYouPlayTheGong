using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class NPCInteractable : InGameObject
{

	public Dialogue StandardDialogue;
	public Dialogue DayOverDialogue;
	public Dialogue PlayingDialogue;
	public List<Dialogue> Dialogues;

	public void Start()
	{
		if(StandardDialogue != null) StandardDialogue.ResetReproduced();
		if(DayOverDialogue != null) DayOverDialogue.ResetReproduced();
		if(PlayingDialogue != null) PlayingDialogue.ResetReproduced();
		foreach (Dialogue d in Dialogues) d.ResetReproduced();
	}
	
	public override void Interact()
	{
		Dialogue lastAvailable = LastAvailableDialogue();
		
		if(DayProgressionManager.Instance.IsDayOver() && DayOverDialogue != null) DayOverDialogue.StartDialogue();
		else if (lastAvailable == null && StandardDialogue != null) StandardDialogue.StartDialogue();
		else if (lastAvailable != null) lastAvailable.StartDialogue();
	}

	public void InstantDialogue(Dialogue dialogue)
	{
		dialogue.StartDialogue();
	}

	private Dialogue LastAvailableDialogue()
	{
		return Dialogues.FindLast(d => d.IsAvailable());
	}

	public override bool IsPlayable()
	{
		if (PlayingDialogue != null) return true;
		return false;
	}

	public override void Play()
	{
		PlayingDialogue.StartDialogue();
	}
	
}
