using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

	public GameObject NotesTab;
	public InventoryListWindow ItemsTab;
	public GameObject MapTab;
	




	

	public void DisplayNotes()
	{
		NotesTab.SetActive(true);
		ItemsTab.gameObject.SetActive(false);
		MapTab.SetActive(false);
	}
	public void DisplayItems()
	{
		NotesTab.SetActive(false);
		ItemsTab.gameObject.SetActive(true);
		MapTab.SetActive(false);
		ItemsTab.UpdateItems();
	}
	public void DisplayMap()
	{
		NotesTab.SetActive(false);
		ItemsTab.gameObject.SetActive(false);
		MapTab.SetActive(true);
	}

}
