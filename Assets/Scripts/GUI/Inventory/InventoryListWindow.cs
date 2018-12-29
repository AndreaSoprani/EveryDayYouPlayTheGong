using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
	private TextMeshProUGUI _description;
	private Image _image;
	private Color _color;
	
	private void Awake()
	{
		_description = GameObject.FindGameObjectWithTag("InventoryDescription").GetComponent<TextMeshProUGUI>();
		_image = GameObject.FindGameObjectWithTag("InventoryImage").GetComponent<Image>();
		_color = _image.color;
		_color = _image.color;
	}

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
		
		_description.text = "";
		_color.a = 0;
		_image.color = _color;
		
		bool first = true;
		foreach (Item item in Player.Instance.GetItem())
		{	
			InventoryListElementController newItem = Instantiate(ItemSlotPrefab);
			newItem.transform.SetParent(Content.transform);
			newItem.SetItem(item, Top.position.y, Bottom.position.y, Scrollbar);
			if (first)
			{
				_color.a = 1;
				_image.color = _color;
				
				_firstElement = newItem;
				_firstElement.GetComponent<Toggle>().Select();
				first = false;
			}
		}

		Scrollbar.value = 1;
	}
}
