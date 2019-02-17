using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{

	public MainMenuController MenuController;
	public Image Black;
	public Image Team;
	public Image Polimi;
	public float OnScreenTime;
	public float TransitionTime;

	private void OnEnable()
	{
		StartCoroutine(StartupRoutine());
	}

	public IEnumerator StartupRoutine()
	{
		Black.enabled = true;
		
		Color tmp = Team.color;
		tmp.a = 0;
		Team.color = tmp;
		Team.enabled = true;
		
		for (float t = 0f; t < 1.0f; t += Time.deltaTime / TransitionTime)
		{
			tmp.a = t;
			Team.color = tmp;
			yield return null;
		}

		tmp.a = 1;
		Team.color = tmp;
		
		yield return new WaitForSeconds(OnScreenTime);
		
		for (float t = 1f; t > 0f; t -= Time.deltaTime / TransitionTime)
		{
			tmp.a = t;
			Team.color = tmp;
			yield return null;
		}

		tmp.a = 0;
		Team.color = tmp;
		
		Team.enabled = false;

		tmp = Polimi.color;
		tmp.a = 0;
		Polimi.color = tmp;
		Polimi.enabled = true;
		
		for (float t = 0f; t < 1.0f; t += Time.deltaTime / TransitionTime)
		{
			tmp.a = t;
			Polimi.color = tmp;
			yield return null;
		}

		tmp.a = 1;
		Polimi.color = tmp;
		
		yield return new WaitForSeconds(OnScreenTime);
		
		for (float t = 1f; t > 0f; t -= Time.deltaTime / TransitionTime)
		{
			tmp.a = t;
			Polimi.color = tmp;
			yield return null;
		}

		tmp.a = 0;
		Polimi.color = tmp;
		
		Polimi.enabled = false;
		
		MenuController.gameObject.SetActive(true);
		
		tmp = Black.color;
		
		for (float t = 1f; t > 0f; t -= Time.deltaTime / TransitionTime)
		{
			tmp.a = t;
			Black.color = tmp;
			yield return null;
		}
		
		MenuController.gameObject.SetActive(true);
		
		Black.enabled = false;
		
		gameObject.SetActive(false);
		
	}
}
