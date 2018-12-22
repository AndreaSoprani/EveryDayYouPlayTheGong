using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class MainMenuOptionsController : MonoBehaviour {

	public Button FirstButton;
	public MainMenuController MainMenu;
	public Settings SettingsFile;
	public Button Up;
	public Button Right;
	public Button Down;
	public Button Left;
	public Button Interact;
	public Button Run;
	public Button Inventory;
	public Button Menu;
	public Button Play;

	private void Start()
	{
		Up.onClick.AddListener(delegate { UpdateCommand("Up", Up);});
		Right.onClick.AddListener(delegate { UpdateCommand("Right", Right);});
		Down.onClick.AddListener(delegate { UpdateCommand("Down", Down);});
		Left.onClick.AddListener(delegate { UpdateCommand("Left", Left);});
		Interact.onClick.AddListener(delegate { UpdateCommand("Interact", Interact);});
		Run.onClick.AddListener(delegate { UpdateCommand("Run", Run);});
		Inventory.onClick.AddListener(delegate { UpdateCommand("Inventory", Inventory);});
		Play.onClick.AddListener(delegate { UpdateCommand("Play", Play);});
		Menu.onClick.AddListener(delegate { UpdateCommand("Menu", Menu);});
		
		
	}

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(FirstButton.gameObject);
		InitializeAllCommand();
	}

	private void Update()
	{
		KeyCode? a=detectPressedKeyOrButton();
		if(a!=null) Debug.Log(a.ToString());
	}

	private KeyCode? detectPressedKeyOrButton()
	{
		
			foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(kcode))
					return kcode;
			}
		

		return null;
	}

	public void UpdateCommand(string commandName,Button button)
	{
		KeyCode? newCommand=detectPressedKeyOrButton();
		while (newCommand==null)
		{
			newCommand=detectPressedKeyOrButton();
		}

		
		switch (commandName)
		{
				case "Up":
					SettingsFile.Up = (KeyCode) newCommand;
					break;
				case "Right":
					SettingsFile.Right = (KeyCode) newCommand;
					break;
				case "Down":
					SettingsFile.Down = (KeyCode) newCommand;
					break;
				case "Left":
					SettingsFile.Left= (KeyCode) newCommand;
					break;
				case "Interact" :
					SettingsFile.Interact= (KeyCode) newCommand;
					break;
				case "Play" :
					SettingsFile.Play= (KeyCode) newCommand;
					break;
				case "Run" :
					SettingsFile.Run= (KeyCode) newCommand;
					break;
				case "Inventory" :
					SettingsFile.ItemsMenu= (KeyCode) newCommand;
					break;
				case "Menu" :
					SettingsFile.Menu= (KeyCode) newCommand;
					break;
				
		}

		button.GetComponent<TextMeshProUGUI>().text = newCommand.ToString();

	}

	private void InitializeAllCommand()
	{
		Up.GetComponent<TextMeshProUGUI>().text = SettingsFile.Up.ToString();
	Right.GetComponent<TextMeshProUGUI>().text = SettingsFile.Right.ToString();
	Down.GetComponent<TextMeshProUGUI>().text = SettingsFile.Down.ToString();
	Left.GetComponent<TextMeshProUGUI>().text = SettingsFile.Left.ToString();
	Interact.GetComponent<TextMeshProUGUI>().text = SettingsFile.Interact.ToString();
	Run.GetComponent<TextMeshProUGUI>().text = SettingsFile.Run.ToString();
	Inventory.GetComponent<TextMeshProUGUI>().text = SettingsFile.ItemsMenu.ToString();
	Menu.GetComponent<TextMeshProUGUI>().text = SettingsFile.Menu.ToString();
	Play.GetComponent<TextMeshProUGUI>().text = SettingsFile.Play.ToString();
	}
}
