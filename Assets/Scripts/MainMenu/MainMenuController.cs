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
	[Header("Fade")]
	public float TimeOut;
	public float TimeIn;
	public float Pause;
	 
	private AsyncOperation _asyncOperation;

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
		_asyncOperation = SceneManager.LoadSceneAsync("BetaScene");
		_asyncOperation.allowSceneActivation = false;
		while (!_asyncOperation.isDone)
		{
			LoadingBar.value = Mathf.Clamp(_asyncOperation.progress,0f,0.9f);
		}

		yield return StartCoroutine(CameraFade.Instance.FadeTo(TimeIn, 1f));
		_asyncOperation.allowSceneActivation = true;
		yield return StartCoroutine(CameraFade.Instance.FadeTo(TimeOut, 0f));
		
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
