using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

	public GameObject NotesTab;
	public GameObject ItemsTab;
	public GameObject MapTab;
	public Player Player;

	private Collection<Item> _items;


	private void Start()
	{
		_items = Player._items;
		ItemsTab.GetComponents<RawImage>();
	}

	public void DisplayNotes()
	{
		NotesTab.SetActive(true);
		ItemsTab.SetActive(false);
		MapTab.SetActive(false);
	}
	public void DisplayItems()
	{
		NotesTab.SetActive(false);
		ItemsTab.SetActive(true);
		MapTab.SetActive(false);
	}
	public void DisplayMap()
	{
		NotesTab.SetActive(false);
		ItemsTab.SetActive(false);
		MapTab.SetActive(true);
	}

}
