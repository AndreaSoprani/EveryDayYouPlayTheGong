using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

	public Button FirstButton;
	public MainMenuOptionsController OptionsMenu;
	public GameObject Buttons;
	public Slider LoadingBar;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(FirstButton.gameObject);
	}

	public void Play()
	{
			Buttons.SetActive(false);
			LoadingBar.gameObject.SetActive(true);
		StartCoroutine(LetTheShowBegin());
	}

	private IEnumerator LetTheShowBegin()
	{
		yield return null;

		//Begin to load the Scene you specify
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BetaScene");
		while (!asyncOperation.isDone)
		{
			LoadingBar.value = asyncOperation.progress;
		}

		yield return null;
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
