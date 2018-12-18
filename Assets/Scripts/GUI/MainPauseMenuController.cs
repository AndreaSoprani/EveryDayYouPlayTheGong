using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPauseMenuController : MonoBehaviour
{
	public OptionsMenuController OptionsMenu;
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
		
		OptionsMenu.gameObject.SetActive(true);
		
		
	}

	
	public void Quit()
	{
		Application.Quit();
	}
}
