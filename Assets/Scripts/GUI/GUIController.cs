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
	public bool DisplayStartupText;
	public MainPauseMenuController PauseMenu;
	public InventoryController Inventory;
	public NotificationController NotificationController;
	public ObjectiveNotificationController ObjectiveNotificationController;
	public DisplayTextScreen DisplayTextScreen;
	public Image[] DayProgressionImages;
	public GameObject DayProgressionGUI;
	public Button FirstPauseButton;
    private float _timePassed;
	private bool _isGamePaused;
	private bool _isShowingStartupScreen;
	

	private void Start()
	{
		if (DisplayStartupText && Settings.StartupText != null)
		{
			DisplayText(Settings.StartupText.text);
		}
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		if (!_isShowingStartupScreen)
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

			if (Input.GetKeyDown(Settings.ItemsMenu) && !PauseMenu.gameObject.activeInHierarchy &&
			    (Inventory.gameObject.activeInHierarchy || !Player.Instance.IsBlocked()))
			{
				if (!Inventory.gameObject.activeInHierarchy)
					OpenInventory();
				else
					CloseInventory();
			}

			if (NotificationController.IsNotificationActive() && NotificationController.CanBeClosed() &&
			    Input.GetKeyDown(Settings.Interact))
			{
				NotificationController.HideNotification();
				Player.Instance.BlockMovement(false);
				_isGamePaused = false;
			}

			NotificationController.DisplayNotification();
		}
		else
		{
			if (DisplayTextScreen.CanBeClosed() && !_isGamePaused && Input.GetKeyDown(KeyCode.X))
			{
				DisplayTextScreen.Close();
				Time.timeScale = 1;
				_isShowingStartupScreen = false;
				
			}
		}

		
		
	}

	private void CloseInventory()
	{
		Inventory.gameObject.SetActive(false);
		Player.Instance.BlockMovement(false);
		_isGamePaused = false;
	}

	private void OpenInventory()
	{
		Inventory.gameObject.SetActive(true);
		Player.Instance.BlockMovement(true);
		_isGamePaused = true;
	}

	private void Pause()
	{
		Time.timeScale = 0;
		_isGamePaused = true;
		PauseMenu.gameObject.SetActive(true);
		DayProgressionGUI.SetActive(false);
		EventSystem.current.SetSelectedGameObject(FirstPauseButton.gameObject);

		
	}

	public void Resume()
	{
		Time.timeScale = 1;
		_isGamePaused = false;
		PauseMenu.gameObject.SetActive(false);
		DayProgressionGUI.SetActive(true);
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
		_isShowingStartupScreen = true;
		Debug.Log("stampooo");
	}
	public void DisplayTextForTime(string text)
	{
		Time.timeScale = 0;
		DisplayTextScreen.Display(text,1f);
		_isShowingStartupScreen = true;
		
	}

	
	public void UpdateDayProgression()
	{

		for(int i = 0; i < DayProgressionImages.Length; i++)
		{
			if(i < DayProgressionManager.Instance.DayProgress) DayProgressionImages[i].gameObject.SetActive(true);
			else DayProgressionImages[i].gameObject.SetActive(false);
		}
	
	}

	
}
