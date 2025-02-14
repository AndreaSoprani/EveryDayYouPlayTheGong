﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPauseMenuController : MonoBehaviour
{
	public GameObject OptionsMenu;
	public GameObject MainMenu;
	public Button FirstPauseButton;
	
	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(FirstPauseButton.gameObject);
		
		
	}

	
	public void LoadGame()
	{
		
	}

	public void ShowOptions()
	{
		MainMenu.SetActive(false);
		OptionsMenu.SetActive(true);
		
		
		
	}

	
	public void Quit()
	{
		Destroy(Player.Instance.gameObject);
		
		Destroy(DayProgressionManager.Instance.gameObject);
		Destroy(QuestManager.Instance.gameObject);
		Destroy(EventManager.Instance.gameObject);
		Destroy(TextBoxManager.Instance.gameObject);
		
		AudioManager.Instance.PlayMusic("Explore");
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
		
	}

	
}
