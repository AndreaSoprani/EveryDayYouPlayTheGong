using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager Instance { get; private set; }

	[Header("Sound Effects")] public AudioSource SoundEffect;
	[Header("Background Music")] public AudioSource BackgroundMusic;

	public AudioClip GongSound;
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void OnEnable()
	{
		EventManager.StartListening("GongPlayed", PlayGong);
	}

	public void PlayGong()
	{
		EventManager.StopListening("GongPlayed", PlayGong);
		SoundEffect.PlayOneShot(GongSound);
		EventManager.StartListening("GongPlayed", PlayGong);
	}
	
}
