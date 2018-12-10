using System;
using System.Collections;
using System.Collections.Generic;
using GUI.Inventory;
using Quests;
using Quests.Objectives;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class GUIController : MonoBehaviour
{

	public Settings Settings;
	public GameObject PauseMenu;
	public Button FirstPauseButton;
	public InventoryController Inventory;
	public NotificationController NotificationController;
	public ObjectiveNotificationController ObjectiveNotificationController;
	public DisplayTextScreen DisplayTextScreen;
	public Image[] DayProgressionImages;
    private float _timePassed;
	private bool _isGamePaused;

	private void Start()
	{
		if (Settings.StartupText != null)
		{
			DisplayText(Settings.StartupText.text);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(Settings.Menu))
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
		if (Input.GetKeyDown(Settings.ItemsMenu) && !PauseMenu.activeInHierarchy)
		{
			if (!Inventory.gameObject.activeInHierarchy)
				OpenInventory();
			else
				CloseInventory();
		}
		
		NotificationController.DisplayNotification();
		
		if (NotificationController.IsNotificationActive() && 
		    NotificationController.CanBeClosed() && 
		    !_isGamePaused && 
		    Input.GetKeyDown(KeyCode.Return))
		{
			NotificationController.HideNotification();
			Time.timeScale = 1;
			_isGamePaused = false;
		}

		if (DisplayTextScreen.IsActive() && 
		    !_isGamePaused && 
		    Input.GetKeyDown(KeyCode.Return))
		{
			DisplayTextScreen.Close();
			Time.timeScale = 1;
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
		Time.timeScale = 0;
		_isGamePaused = true;
	}

	private void Pause()
	{
		Time.timeScale = 0;
		_isGamePaused = true;
		PauseMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(FirstPauseButton.gameObject);
		
	}

	public void Resume()
	{
		Time.timeScale = 1;
		_isGamePaused = false;
		PauseMenu.SetActive(false);
	}

	public void NotifyGetItem(Item item)
	{
		NotificationController.InsertNotification(new ItemNotification(item, true));
	}

	public void NotifyRemoveItem(Item item)
	{
		NotificationController.InsertNotification(new ItemNotification(item, false));
	}
	
	public void NotifyQuestActivated(Quest quest)
	{
		NotificationController.InsertNotification(new QuestNotification(quest, true));
	}

	public void NotifyQuestCompleted(Quest quest)
	{
		NotificationController.InsertNotification(new QuestNotification(quest, false));
	}

	public void NotifyNewObjective(string objectiveDescription)
	{
		ObjectiveNotificationController.ShowNewObjective(objectiveDescription);
	}

	public void DisplayText(string text)
	{
		Time.timeScale = 0;
		DisplayTextScreen.Display(text);
	}

	
	public void UpdateDayProgression()
	{
		Debug.Log("Inside Day Progression Update");

		for(int i = 0; i < DayProgressionImages.Length; i++)
		{
			if(i < DayProgressionManager.Instance.DayProgress) DayProgressionImages[i].gameObject.SetActive(true);
			else DayProgressionImages[i].gameObject.SetActive(false);
		}
	
	}

	public void LoadGame()
	{
		
	}

	public void Options()
	{
		
	}

	public void Quit()
	{
		Application.Quit();
	}
}
