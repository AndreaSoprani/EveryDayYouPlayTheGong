using System.Linq;
using UnityEngine;
using Utility;

public class AudioManager : MonoBehaviour
{

	private static AudioManager _instance;
	public static AudioManager Instance { get { return _instance; } }
	
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		AkSoundEngine.PostEvent("Explore",GameObject.FindGameObjectWithTag("MainCamera"));
	}

	public Settings Settings;

	/// <summary>
	/// Method used to play a specific sound.
	/// </summary>
	/// <param name="sound">The sound to be played.</param>
	public void PlaySound(Sound sound)
	{
		if (!Settings.SFXOn || sound == null) return;

		if (!sound.HasSource())
		{
			GameObject go = new GameObject("AudioSource_" + sound.Name);
			go.transform.SetParent(this.transform);
			sound.SetSource(go.AddComponent<AudioSource>());
		}
		
		sound.Play(Settings.SFXVolume);
		
	}
	
	
}
