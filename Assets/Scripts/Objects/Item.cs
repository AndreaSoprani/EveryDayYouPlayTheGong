using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Item : ScriptableObject
{

	// ITEM ATTRIBUTES

	public string Id; // Unique ID of the item.
	public string Name; // Name of the item, displayed in the item menu.
	public string Description; // Description for the item, displayed in the item menu.
	
	// IMAGE ATTRIBUTES

	public Sprite MenuArtwork; // Artwork displayed in the item menu.
	public Sprite PickupArtwork; // Artwork displayed when the item has to be picked up.

	
}
