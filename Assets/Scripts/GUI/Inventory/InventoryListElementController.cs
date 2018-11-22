using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListElementController : MonoBehaviour
{

	private Item _item;
	public Image Image;
	public Text NameField;

	

	public void SetItem(Item item)
	{
		_item = item;
		NameField.text = _item.Name;
		Image.sprite = _item.InventoryArtwork;
	}

	public void ShowInfo()
	{
		GameObject.FindGameObjectWithTag("InventoryDescription").GetComponent<Text>().text = _item.Description;
		GameObject.FindGameObjectWithTag("InventoryImage").GetComponent<Image>().sprite = _item.InventoryArtwork;
		
	}
}
