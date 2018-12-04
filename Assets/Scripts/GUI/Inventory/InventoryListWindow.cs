using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListWindow : MonoBehaviour
{

	public InventoryListElementController ItemSlotPrefab;
	public GameObject Content;


	private InventoryListElementController _firstElement;

	private void OnEnable()
	{
		UpdateItems();
	}

	public void UpdateItems()
	{
	
		foreach (Transform child in Content.transform)
		{
			Destroy(child.gameObject);
			
		}

		bool first = true;
		foreach (Item item in Player.Instance.GetItem())
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
