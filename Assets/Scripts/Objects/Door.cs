using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractiveObject
{
	
	public bool IsOpen;

	private void Start()
	{
		if(IsOpen) OpenDoor();
		else CloseDoor();
	}

	public bool IsInteractable()
	{
		//For now doors are interactable only if they are closed.
		return !IsOpen;
	}

	public void Interact(Player player)
	{
		OpenDoor();
	}

	/// <summary>
	/// Method used to Open a door.
	/// </summary>
	public void OpenDoor()
	{
		//TODO animation (for now it just disappears).
		GetComponent<SpriteRenderer>().enabled = false;
		
		GetComponent<BoxCollider2D>().enabled = false;
		IsOpen = true;
	}
	
	/// <summary>
	/// Method used to close a door.
	/// </summary>
	public void CloseDoor()
	{
		//TODO animation (for now it just re-appears).
		GetComponent<SpriteRenderer>().enabled = true;
		
		GetComponent<BoxCollider2D>().enabled = true;
		IsOpen = false;
	}
}
