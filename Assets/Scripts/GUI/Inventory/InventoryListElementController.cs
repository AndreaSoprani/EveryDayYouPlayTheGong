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

	private float _top;
	private float _bottom;
	private float _offset;
	
	private Scrollbar _scrollbar;

	public void SetItem(Item item, float marginTop, float marginBottom, Scrollbar scrollbar)
	{
		_item = item;
		NameField.text = _item.Name;
		//Image.sprite = _item.InventoryArtwork;
		_top = marginTop;
		_bottom = marginBottom;
		_scrollbar = scrollbar;
	}

	public void ShowInfo()
	{
		if (transform.position.y > _top)
			_scrollbar.value += 0.28f;

		if (transform.position.y < _bottom)
			_scrollbar.value -= 0.28f;
		
		GameObject.FindGameObjectWithTag("InventoryDescription").GetComponent<TextMeshProUGUI>().text = _item.Description;
		GameObject.FindGameObjectWithTag("InventoryImage").GetComponent<Image>().sprite = _item.InventoryArtwork;
		
	}
}
