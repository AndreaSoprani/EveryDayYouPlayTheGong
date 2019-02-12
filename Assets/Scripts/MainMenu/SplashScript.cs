using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{

	public MainMenuController MenuController;
	public Image Team;
	public Image Polimi;
	public float OnScreenTime;

	private void OnEnable()
	{
		StartCoroutine(StartupRoutine());
	}

	public IEnumerator StartupRoutine()
	{
		Team.enabled = true;
		yield return new WaitForSeconds(OnScreenTime);
		Polimi.enabled = true;
		Team.enabled = false;
		yield return new WaitForSeconds(OnScreenTime);
		MenuController.gameObject.SetActive(true);
		Polimi.enabled = false;
		gameObject.SetActive(false);
		
	}
}
