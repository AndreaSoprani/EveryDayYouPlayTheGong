using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class InventoryListWindow : MonoBehaviour
{

	public InventoryListElementController ItemSlotPrefab;
	public GameObject Content;
	public Transform Top;
	public Transform Bottom;
	public Scrollbar Scrollbar;

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
			newItem.SetItem(item, Top.position.y, Bottom.position.y, Scrollbar);
			if (first)
			{
				_firstElement = newItem;
				_firstElement.GetComponent<Toggle>().Select();
				first = false;
			}
		}

		Scrollbar.value = 1;
	}
}
