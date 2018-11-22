using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
	
	// ITEM ATTRIBUTES

	public string Id; // Unique ID of the item.
	public string Name; // Name of the item, displayed in the item menu.
	[TextArea]
	public string Description; // Description for the item, displayed in the item menu.
	
	// IMAGE ATTRIBUTES

	public Sprite PickupArtwork; // Artwork displayed in the item menu.
	public Sprite InventoryArtwork; // Artwork displayed when the item has to be picked up.

	
}
