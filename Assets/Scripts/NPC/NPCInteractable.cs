using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class NPCInteractable : InGameObject
{

	public List<Dialogue> Dialogues;
	
	public override void Interact(Player player)
	{
		LastAvailableDialogue().StartDialogue();
	}

	private Dialogue LastAvailableDialogue()
	{
		return Dialogues.FindLast(d => d.IsAvailable());
	}
}
