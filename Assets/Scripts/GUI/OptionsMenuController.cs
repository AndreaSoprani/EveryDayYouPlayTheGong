using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class OptionsMenuController : MonoBehaviour
{

	public Slider MusicVolume;
	public Slider EffectsVolume;
	public GameObject MainPauseMenu;
	public Button FirstButton;
	public Settings GameSettings;

	private void OnEnable()
	{
		MainPauseMenu.SetActive(false);
		EventSystem.current.SetSelectedGameObject(MusicVolume.gameObject);
		MusicVolume.value = GameSettings.MusicVolume;
		EffectsVolume.value = GameSettings.SFXVolume;
		
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
