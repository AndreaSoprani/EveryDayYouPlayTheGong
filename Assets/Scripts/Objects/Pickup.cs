using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Pickup : MonoBehaviour, IInteractiveObject
{

	public Item Item;
	
	// Use this for initialization
	void Start ()
	{
		this.GetComponent<SpriteRenderer>().sprite = Item.PickupArtwork;
		this.name = "Pickup" + Item.Id;
	}

	public bool IsInteractable()
	{
		return true;
	}

	public void Interact(Player player)
	{
		player.AddItem(Item);
		Destroy(gameObject);
	}
}
