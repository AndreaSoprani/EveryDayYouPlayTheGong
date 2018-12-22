using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{

	public Slider MusicVolume;
	public Slider EffectsVolume;
	public GameObject MainPauseMenu;
	public Button FirstButton;

	private void OnEnable()
	{
		MainPauseMenu.SetActive(false);
		EventSystem.current.SetSelectedGameObject(MusicVolume.gameObject);
		MusicVolume.value = AudioManager.Instance.MusicVolume;
		EffectsVolume.value = AudioManager.Instance.EffectVolume;
		
	}

	public void ChangeMusicVolume()
	{
		
		AudioManager.Instance.ChangeVolume(SoundType.Music,(int) MusicVolume.value);
	}
	public void ChangeEffectsVolume()
	{
		
		AudioManager.Instance.ChangeVolume(SoundType.Effects,(int) EffectsVolume.value);
	}

	public void HideOptions()
	{
		gameObject.SetActive(false);
		MainPauseMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(FirstButton.gameObject);
		
	}
}
