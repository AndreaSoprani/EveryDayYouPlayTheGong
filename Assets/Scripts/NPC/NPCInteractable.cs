using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class NPCInteractable : InGameObject
{

	public Dialogue StandardDialogue;
	public List<Dialogue> Dialogues;
	
	public override void Interact()
	{
		Dialogue lastAvailable = LastAvailableDialogue();

		if (lastAvailable != null)
		{
			lastAvailable.StartDialogue();
		} 
		else if (StandardDialogue != null)
		{
			StandardDialogue.StartDialogue();
		}

	}

	private Dialogue LastAvailableDialogue()
	{
		return Dialogues.FindLast(d => d.IsAvailable());
	}
}
