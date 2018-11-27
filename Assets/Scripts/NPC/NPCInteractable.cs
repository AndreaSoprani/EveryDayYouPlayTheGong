using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class NPCInteractable : InGameObject
{

	public Dialogue Dialogue;
	
	public override void Interact(Player player)
	{
		Dialogue.StartDialogue();
	}
}
