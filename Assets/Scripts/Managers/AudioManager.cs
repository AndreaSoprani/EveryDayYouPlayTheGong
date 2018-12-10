using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

public class AudioManager : MonoBehaviour
{

	private static AudioManager _instance;

	public static AudioManager Instance
	{
		get { return _instance; }
	}

	[Range(0, 100)] public int MusicVolume;
	[Range(0, 100)] public int EffectVolume;
	private uint _bankIDMain;
	private uint _bankIDGong;
	private uint _bankIDBells;
	private uint _bankIDXylophone;

	private string _currentlyPlaying;
	private bool _isMusicMuted;
	private Dictionary<string, bool> _isAlreadyPlayed;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(this.gameObject);
		_isAlreadyPlayed = new Dictionary<string, bool>()
		{
			{"Explore", false},
			{"Dungeon", false},
			{"Silence", false}
		};
	}

	private void Start()
	{
		AkSoundEngine.LoadBank("Main", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDMain);
		AkSoundEngine.LoadBank("Gong", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDGong);
		AkSoundEngine.LoadBank("Bell", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDBells);
		AkSoundEngine.LoadBank("Xylophone", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDXylophone);
		/*AkSoundEngine.SetRTPCValue("MusicVolume", MusicVolume);
		AkSoundEngine.SetRTPCValue("EffectVolume", EffectVolume);*/
		AkSoundEngine.PostEvent("Explore", gameObject);
		_currentlyPlaying = "Explore";

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

	public void PlayEvent(string sound)
	{
		AkSoundEngine.PostEvent(sound, gameObject);
	}

	public void PlayMusic(string sound)
	{
		/*Debug.Log("Explore "+_isAlreadyPlayed["Explore"]+" Dungeon "+_isAlreadyPlayed["Dungeon"]+ " Silence "+_isAlreadyPlayed["Silence"]);
		if (sound == "Explore" && _currentlyPlaying!="Explore")
		{
			PauseEvent(_currentlyPlaying, 1);
			
		}

		if (_isAlreadyPlayed[sound])
		{
			ResumeEvent(sound, 1);
		
		}
		else
		{
			_isAlreadyPlayed[sound] = true;
			PlayEvent(sound);
		}

		_currentlyPlaying = sound;
		Debug.Log(_currentlyPlaying);*/
		if (!_isMusicMuted)
		{
			if (sound == "Explore")
			{
				StopEvent(_currentlyPlaying, 0);

			}

			PlayEvent(sound);
		}

		_currentlyPlaying = sound;
		Debug.Log(_currentlyPlaying);

	}

	

	public void StopEvent(string eventName,int fadeout)
	{
		uint eventId = AkSoundEngine.GetIDFromString(eventName);
		AkSoundEngine.ExecuteActionOnEvent(eventId, AkActionOnEventType.AkActionOnEventType_Stop, gameObject,fadeout*1000,AkCurveInterpolation.AkCurveInterpolation_Sine);
	}
	public void PauseEvent(string eventName,int fadeout)
	{
		uint eventId = AkSoundEngine.GetIDFromString(eventName);
		AkSoundEngine.ExecuteActionOnEvent(eventId, AkActionOnEventType.AkActionOnEventType_Pause, gameObject,fadeout*1000,AkCurveInterpolation.AkCurveInterpolation_Sine);
	}
	public void ResumeEvent(string eventName,int fadeout)
	{
		uint eventId = AkSoundEngine.GetIDFromString(eventName);
		AkSoundEngine.ExecuteActionOnEvent(eventId, AkActionOnEventType.AkActionOnEventType_Resume, gameObject,fadeout*1000,AkCurveInterpolation.AkCurveInterpolation_Sine);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			if (_isMusicMuted)
			{
				ResumeEvent("Explore",0);
				if(_currentlyPlaying!="Explore") PlayEvent(_currentlyPlaying);
				
			}
			else
			{
				PauseEvent("Explore",0);
			}
			_isMusicMuted = !_isMusicMuted;
		}
		
		
	}
}
