using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

	public Button FirstButton;
	public MainMenuOptionsController OptionsMenu;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(FirstButton.gameObject);
	}

	public void Play()
	{
			
	}

	public void Options()
	{
		OptionsMenu.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit();
	}
	
}
