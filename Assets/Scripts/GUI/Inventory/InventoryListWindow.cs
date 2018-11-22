using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListWindow : MonoBehaviour
{

	public InventoryListElementController ItemSlotPrefab;
	public GameObject Content;
	public Player player;


	

	public void UpdateItems()
	{
		Debug.Log("ciao");
		foreach (Transform child in Content.transform)
		{
			Destroy(child.gameObject);
			Debug.Log("cancellato");
		}
		/*InventoryListElementController newItem = Instantiate(ItemSlotPrefab);
		newItem.transform.SetParent(Content.transform);
		newItem.SetItem(player._items[player._items.Count-1]);*/
		foreach (Item item in player._items)
		{
			Debug.Log(item.ToString());
			InventoryListElementController newItem = Instantiate(ItemSlotPrefab);
			newItem.transform.SetParent(Content.transform);
			newItem.SetItem(item);
		}
	}
	
}
