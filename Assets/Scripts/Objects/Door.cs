using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class Door : InGameObject
{
	
	public bool IsOpen;

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

	public override void Interact(Player player)
	{
		OpenDoor();
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
