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

	private InventoryListElementController _firstElement;

	

	public void UpdateItems()
	{
	
		foreach (Transform child in Content.transform)
		{
			Destroy(child.gameObject);
			
		}

		bool first = true;
		foreach (Item item in player._items)
		{
			
			InventoryListElementController newItem = Instantiate(ItemSlotPrefab);
			newItem.transform.SetParent(Content.transform);
			newItem.SetItem(item);
			if (first)
			{
				_firstElement = newItem;
				_firstElement.GetComponent<Toggle>().Select();
				first = false;
			}
		}
	}
	
}
