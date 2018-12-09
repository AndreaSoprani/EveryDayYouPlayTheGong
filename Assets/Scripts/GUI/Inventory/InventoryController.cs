using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.Experimental.UIElements.Button;

public class InventoryController : MonoBehaviour
{

	public NotesListWindow NotesTab;
	public InventoryListWindow ItemsTab;
	public GameObject MapTab;
	public Toggle NotesButton;
	public Button ItemsButton;
	public Button MapButton;
	
	




	

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

	private void Update()
	{
		if (isActiveAndEnabled)
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{	if (NotesTab.isActiveAndEnabled)
				{
					DisplayItems();
				}
				else if (ItemsTab.isActiveAndEnabled)
				{
					DisplayMap();
				}
				else
				{
					
					DisplayNotes();
				}
				
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (NotesTab.isActiveAndEnabled)
				{
					DisplayMap();
				}
				else if (ItemsTab.isActiveAndEnabled)
				{
					DisplayNotes();
				}
				else
				{
					DisplayItems();
				}
			}
		}

		
	}
}
