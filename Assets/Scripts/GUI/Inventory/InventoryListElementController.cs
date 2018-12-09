using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListElementController : MonoBehaviour
{

	private Item _item;
	public Image Image;
	public TextMeshProUGUI NameField;

	

	public void SetItem(Item item)
	{
		
		_item = item;
		NameField.text = _item.Name;
		Image.sprite = _item.InventoryArtwork;
		Debug.Log(NameField.text + "  "+ _item.Name);
	}

	public void ShowInfo()
	{
		GameObject.FindGameObjectWithTag("InventoryDescription").GetComponent<TextMeshProUGUI>().text = _item.Description;
		GameObject.FindGameObjectWithTag("InventoryImage").GetComponent<Image>().sprite = _item.InventoryArtwork;
		
	}
}
