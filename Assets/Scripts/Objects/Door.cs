using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utility;

public class Door : InGameObject
{

	public bool IsOpen;
	public bool Secret;
	public bool OpenByScript;
	public Item ItemNeeded;
	public Dialogue ClosedDoorDialogue;

	private void Start()
	{
		if(IsOpen) OpenDoor();
		else CloseDoor();
	}

	public override bool IsInteractable()
	{
		return !IsOpen && !Secret;
	}

	public override void Interact()
	{
		// If no item is needed open the door.
		if (!OpenByScript && ItemNeeded == null) OpenDoor();
		else if (!OpenByScript && Player.Instance.HasItem(ItemNeeded)) // Check if the player has the item, otherwise start the dialogue.
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
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(sr != null) sr.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		IsOpen = true;
	}
	
	/// <summary>
	/// Method used to close a door.
	/// </summary>
	public void CloseDoor()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(sr != null) sr.enabled = true;
		GetComponent<Collider2D>().enabled = true;
		IsOpen = false;
	}
}
