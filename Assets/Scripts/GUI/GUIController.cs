using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	
	
	public GameObject PauseMenu;
	public InventoryController Inventory;
	public NotificationController NotificationController;
	
    private float _timePassed;
	private bool _isGamePaused;
	

	private void OnEnable()
	{
		EventManager.StartListening("InteractionGUIElement", DisplayInteraction);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_isGamePaused)
			{
				if (Inventory.gameObject.activeInHierarchy)
					CloseInventory();
				else
					Resume();
			}
			else
			{
					Pause();
			}
		}
		if (Input.GetKeyDown(KeyCode.I) && !PauseMenu.activeInHierarchy)
		{
			if (!Inventory.gameObject.activeInHierarchy)
				OpenInventory();
			else
				CloseInventory();
		}
		
		if (NotificationController.IsNotificationActive() && Input.GetKeyDown(KeyCode.Space))
		{
			NotificationController.HideNotification();
			Time.timeScale = 1;
			_isGamePaused = false;
		}	
		
	}

	private void CloseInventory()
	{
		Inventory.gameObject.SetActive(false);
		Time.timeScale = 1;
		_isGamePaused = false;
	}

	private void OpenInventory()
	{
		Inventory.gameObject.SetActive(true);
		Inventory.ItemsTab.UpdateItems();
		Time.timeScale = 0;
		_isGamePaused = true;
	}

	private void Pause()
	{
		Time.timeScale = 0;
		_isGamePaused = true;
		PauseMenu.SetActive(true);
	}

	public void Resume()
	{
		Time.timeScale = 1;
		_isGamePaused = false;
		PauseMenu.SetActive(false);
	}

	public void NotifyGetItem(Item item)
	{
		Time.timeScale = 0;
		NotificationController.ShowNotification(item, true);
	}

	public void NotifyRemoveItem(Item item)
	{
		Time.timeScale = 0;
		NotificationController.ShowNotification(item, false);
	}
	
	/// <summary>
	/// Used to display the option to interact with an object on screen.
	/// </summary>
	void DisplayInteraction()
	{
		//Debug.Log("Press KEY to interact."); 
		//TODO: Retrieve correct key.
		//TODO: Display it on the screen.
	}

	public void QuitGame()
	{
		Debug.Log("NOPEEEE");
	}
	
	public void LoadGame()
	{
		Debug.Log("Load");
	}
	public void Options()
	{
		Debug.Log("Options");
	}
	
}
