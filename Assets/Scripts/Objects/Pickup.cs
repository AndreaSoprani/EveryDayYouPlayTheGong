using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
[ExecuteInEditMode]
public class Pickup : InGameObject
{

	public Item Item;
	
	
	// Use this for initialization
	void Start ()
	{
		this.GetComponent<SpriteRenderer>().sprite = Item.PickupArtwork;
		this.name = "Pickup" + Item.Id;
	}

	public override void Interact(Player player)
	{
		player.AddItem(Item);
		Destroy(gameObject);
	}
}
