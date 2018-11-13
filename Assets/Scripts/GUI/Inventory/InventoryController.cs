using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

	public GameObject NotesTab;
	public GameObject ItemsTab;
	public GameObject MapTab;

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
