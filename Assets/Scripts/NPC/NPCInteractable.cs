using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class NPCInteractable : MonoBehaviour, IInteractiveObject
{

	public Dialogue Dialogue;

	public bool IsInteractable()
	{
		return true;
	}

	public void Interact(Player player)
	{
		Dialogue.StartDialogue();
	}
}
