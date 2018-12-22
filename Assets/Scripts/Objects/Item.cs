using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
public class Item : ScriptableObject
{
	
	// ITEM ATTRIBUTES

	public string Id; // Unique ID of the item.
	public string Name; // Name of the item, displayed in the item menu.
	[TextArea]
	public string Description; // Description for the item, displayed in the item menu.
	
	// IMAGE ATTRIBUTES

	public Sprite InventoryArtwork; // Artwork displayed when the item has to be picked up.

	
}
