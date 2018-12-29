using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class OptionsMenuController : MonoBehaviour
{

	public GameObject MainMenu;
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
	public GameObject Halo;
	public Slider MusicVolume;
	public Slider EffectsVolume;
	public Button FirstPauseButton;
	public TextMeshProUGUI Text;

	
	private bool _isChangingCommand;
	private string _commandToBeChangedString;
	private Button _commandToBeChangedButton;
	private bool _canRead;

	private void Start()
	{
		Up.onClick.AddListener(delegate { PrepareToChangeCommand("Up", Up); });
		
		Right.onClick.AddListener(delegate { PrepareToChangeCommand("Right", Right);});
		Down.onClick.AddListener(delegate { PrepareToChangeCommand("Down", Down);});
		Left.onClick.AddListener(delegate { PrepareToChangeCommand("Left", Left);});
		Interact.onClick.AddListener(delegate { PrepareToChangeCommand("Interact", Interact);});
		Run.onClick.AddListener(delegate { PrepareToChangeCommand("Run", Run);});
		Inventory.onClick.AddListener(delegate { PrepareToChangeCommand("Inventory", Inventory);});
		Play.onClick.AddListener(delegate { PrepareToChangeCommand("Play", Play);});
		Menu.onClick.AddListener(delegate { PrepareToChangeCommand("Menu", Menu);});
		EventSystem.current.SetSelectedGameObject(Up.gameObject);
		
	}

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(Up.gameObject);
		InitializeAllCommand();
	}

	private void Update()
	{
		if (_isChangingCommand)
		{
			
			KeyCode? newCommand=detectPressedKeyOrButton();
			if (newCommand != null && _canRead)
			{
					Debug.Log(newCommand.Value);
					UpdateCommand(_commandToBeChangedString,_commandToBeChangedButton,newCommand.Value);
				

					
			}
		}
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

	public void UpdateCommand(string commandName,Button button, KeyCode newCommand)
	{
		
		if(CanBeSet(commandName,newCommand))
		{
			switch (commandName)
			{
					case "Up":
						SettingsFile.Up = newCommand;
						Player.Instance.RefreshCommand();
						break;
					case "Right":
						SettingsFile.Right = newCommand;
						Player.Instance.RefreshCommand();
						break;
					case "Down":
						SettingsFile.Down = newCommand;
						Player.Instance.RefreshCommand();
						break;
					case "Left":
						SettingsFile.Left= newCommand;
						Player.Instance.RefreshCommand();
						break;
					case "Interact" :
						SettingsFile.Interact= newCommand;
						break;
					case "Play" :
						SettingsFile.Play= newCommand;
						break;
					case "Run" :
						SettingsFile.Run= newCommand;
						break;
					case "Inventory" :
						SettingsFile.ItemsMenu= newCommand;
						break;
					case "Menu" :
						SettingsFile.Menu= newCommand;
						break;
					
			}
			button.GetComponentInChildren<TextMeshProUGUI>().text = newCommand.ToString();

			Halo.SetActive(false);
			_isChangingCommand = false;
			button.interactable = true;
			EventSystem.current.SetSelectedGameObject(button.gameObject);

		}
		else
		{
			StartCoroutine(ShowFailed());
		}

		}

	private void PrepareToChangeCommand(string command, Button button)
	{
		Halo.SetActive(true);
		_isChangingCommand = true;
		_canRead = false;
		_commandToBeChangedButton = button;
		_commandToBeChangedString = command;
		button.interactable = false;
		StartCoroutine(WaitBeforeCommand(0.1f));
	}
	private bool CanBeSet(string commandName, KeyCode candidate)
	{
		switch (commandName)
		{
			case "Up":
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run &&  candidate != SettingsFile.ItemsMenu;
			case "Right":
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Down":
				return  candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Left":
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Interact" :
				return candidate != SettingsFile.Down &&  candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Play" :
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu &&  candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Run" :
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu;
			case "Inventory" :
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Menu && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up ;
			case "Menu" :
				return candidate != SettingsFile.Down && candidate != SettingsFile.Interact && candidate != SettingsFile.Left && candidate != SettingsFile.Play && candidate != SettingsFile.Right && candidate != SettingsFile.Run && candidate != SettingsFile.Up && candidate != SettingsFile.ItemsMenu ;
					
		}

		return false;
	}
	private IEnumerator ShowFailed()
	{
		Text.text = "Button already used";
		_isChangingCommand = false;
		yield return new WaitForSecondsRealtime(2f);
		
		Halo.SetActive(false);
		
		_commandToBeChangedButton.interactable = true;
		EventSystem.current.SetSelectedGameObject(_commandToBeChangedButton.gameObject);
	}

	public void HideOptions()
	{
		gameObject.SetActive(false);
		MainMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(FirstPauseButton.gameObject);
	}
	public void ResetCommand()
	{
		SettingsFile.Up = KeyCode.UpArrow;
		SettingsFile.Down = KeyCode.DownArrow;
		SettingsFile.Right = KeyCode.RightArrow;
		SettingsFile.Left = KeyCode.LeftArrow;

		SettingsFile.Run = KeyCode.LeftControl;
		SettingsFile.Interact = KeyCode.X;
		SettingsFile.Play = KeyCode.Space;

		SettingsFile.ItemsMenu = KeyCode.Z;
		SettingsFile.Menu = KeyCode.Escape;

		EffectsVolume.value =100 ;
		MusicVolume.value = 52;
		
		InitializeAllCommand();
	}
	private void InitializeAllCommand()
	{
		Up.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Up.ToString();
		Right.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Right.ToString();
		Down.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Down.ToString();
		Left.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Left.ToString();
		Interact.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Interact.ToString();
		Run.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Run.ToString();
		Inventory.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.ItemsMenu.ToString();
		Menu.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Menu.ToString();
		Play.GetComponentInChildren<TextMeshProUGUI>().text = SettingsFile.Play.ToString();
		MusicVolume.value = SettingsFile.MusicVolume;
		EffectsVolume.value = SettingsFile.SFXVolume;
	}

	public void ChangeMusicVolume()
	{
		
		AudioManager.Instance.ChangeVolume(SoundType.Music,(int) MusicVolume.value);
	}
	public void ChangeEffectsVolume()
	{
		
		AudioManager.Instance.ChangeVolume(SoundType.Effects,(int) EffectsVolume.value);
	}

	public void TestSound()
	{
		AudioManager.Instance.PlayEvent("MainGong");
	}

	private IEnumerator WaitBeforeCommand(float seconds)
	{
		Debug.Log("Start waiting");
		yield return new WaitForSecondsRealtime(seconds);
		_canRead = true;
		Debug.Log("Finish waiting");
	}

}
