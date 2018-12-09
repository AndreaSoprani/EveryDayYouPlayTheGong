using System;
using System.Collections;
using System.Collections.Generic;
using GUI.Inventory;
using Quests;
using Quests.Objectives;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GUIController : MonoBehaviour
{

	public Settings Settings;
	public GameObject PauseMenu;
	public InventoryController Inventory;
	public NotificationController NotificationController;
	public ObjectiveNotificationController ObjectiveNotificationController;
	public Image[] DayProgressionImages;
    private float _timePassed;
	private bool _isGamePaused;

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
		
		if (NotificationController.IsNotificationActive() && NotificationController.CanBeClosed() && Input.GetKeyDown(KeyCode.Return))
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
		NotificationController.ShowItemNotification(item, true);
	}

	public void NotifyRemoveItem(Item item)
	{
		Time.timeScale = 0;
		NotificationController.ShowItemNotification(item, false);
	}
	
	public void NotifyQuestActivated(Quest quest)
	{
		Time.timeScale = 0;
		NotificationController.ShowQuestNotification(quest, true);
	}

	public void NotifyQuestCompleted(Quest quest)
	{
		Time.timeScale = 0;
		NotificationController.ShowQuestNotification(quest, false);
	}

	public void NotifyNewObjective(string objectiveDescription)
	{
		ObjectiveNotificationController.ShowNewObjective(objectiveDescription);
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

	public void UpdateDayProgression()
	{
		Debug.Log("Inside Day Progression Update");

		for(int i = 0; i < DayProgressionImages.Length; i++)
		{
			if(i < DayProgressionManager.Instance.DayProgress) DayProgressionImages[i].gameObject.SetActive(true);
			else DayProgressionImages[i].gameObject.SetActive(false);
		}
	
	}
}
