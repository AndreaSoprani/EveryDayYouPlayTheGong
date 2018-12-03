using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class NPCInteractable : InGameObject
{

	public Dialogue StandardDialogue;
	public Dialogue DayOverDialogue;
	public List<Dialogue> Dialogues;
	
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
}
