using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

	public NotesListWindow NotesTab;
	public InventoryListWindow ItemsTab;
	public GameObject MapTab;
	




	

	public void DisplayNotes()
	{
		NotesTab.gameObject.SetActive(true);
		ItemsTab.gameObject.SetActive(false);
		MapTab.SetActive(false);
		NotesTab.UpdateItems();
	}
	public void DisplayItems()
	{
		NotesTab.gameObject.SetActive(false);
		ItemsTab.gameObject.SetActive(true);
		MapTab.SetActive(false);
		ItemsTab.UpdateItems();
	}
	public void DisplayMap()
	{
		NotesTab.gameObject.SetActive(false);
		ItemsTab.gameObject.SetActive(false);
		MapTab.SetActive(true);
	}

	public void CloseEverything()
	{
		NotesTab.gameObject.SetActive(false);
		ItemsTab.gameObject.SetActive(false);
		MapTab.SetActive(false);
	}
}
