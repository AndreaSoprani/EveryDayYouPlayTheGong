using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class Door : InGameObject
{

	public bool IsOpen;
	public Item ItemNeeded;
	public Dialogue ClosedDoorDialogue;

	private void Start()
	{
		if(IsOpen) OpenDoor();
		else CloseDoor();
	}

	public override bool IsInteractable()
	{
		//For now doors are interactable only if they are closed.
		return !IsOpen;
	}

	public override void Interact()
	{
		// If no item is needed open the door.
		if (ItemNeeded == null)
		{
			OpenDoor();
			return;
		}
		
		// Check if the player has the item, otherwise start the dialogue.
		if (Player.Instance.HasItem(ItemNeeded))
		{
			Player.Instance.RemoveItem(ItemNeeded);
			ItemNeeded = null;
			OpenDoor();
		}
		else if (ClosedDoorDialogue != null)
		{
			ClosedDoorDialogue.StartDialogue();
		}
	}

	/// <summary>
	/// Method used to Open a door.
	/// </summary>
	public void OpenDoor()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
		IsOpen = true;
	}
	
	/// <summary>
	/// Method used to close a door.
	/// </summary>
	public void CloseDoor()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<BoxCollider2D>().enabled = true;
		IsOpen = false;
	}
}
