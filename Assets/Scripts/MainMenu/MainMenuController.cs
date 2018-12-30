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
	public GameObject LoadingBarObject; 
	public Slider LoadingBarSlider;
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
			LoadingBarObject.gameObject.SetActive(true);
		//SceneManager.LoadScene("BetaScene");
		StartCoroutine(LetTheShowBegin());
	}

	private IEnumerator LetTheShowBegin()
	{
		yield return null;

		float progress = 0;
		//Begin to load the Scene you specify
		_asyncOperation = SceneManager.LoadSceneAsync("BetaScene");
		//_asyncOperation.allowSceneActivation = false;
		while (progress!=1)
		{
			progress=Mathf.Clamp01(_asyncOperation.progress / 0.9f);
			LoadingBarSlider.value = progress;
			Debug.Log(progress);
			yield return null;
		}

		/*yield return StartCoroutine(CameraFade.Instance.FadeTo(TimeIn, 1f));
		yield return new WaitForSeconds(Pause);
		_asyncOperation.allowSceneActivation = true;
		yield return StartCoroutine(CameraFade.Instance.FadeTo(TimeOut, 0f));*/
		
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
