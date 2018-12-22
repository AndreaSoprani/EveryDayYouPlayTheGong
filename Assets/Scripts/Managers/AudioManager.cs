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
	private uint _bankIDFX;
	

	private string _currentlyPlaying;

	

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
		AkSoundEngine.LoadBank("Main", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDMain);
		AkSoundEngine.LoadBank("Effects", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankIDFX);
		
		AkSoundEngine.SetRTPCValue("MusicVolume", MusicVolume);
		AkSoundEngine.SetRTPCValue("FXVolume", EffectVolume);
		AkSoundEngine.PostEvent("PlayMusic", gameObject);
		_currentlyPlaying = "Explore";

	}

    public void ChangeVolume(SoundType  type,int volume)
    {
	    if (type == SoundType.Music)
	    {
		    AkSoundEngine.SetRTPCValue("MusicVolume", volume);
		    MusicVolume = volume;
	    }
	    else
	    {
		    AkSoundEngine.SetRTPCValue("FXVolume", volume);
		    EffectVolume = volume;
	    }
	    
	    
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
		Debug.Log(sound);
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
		if(Input.GetKeyDown(KeyCode.H))
			PlayEvent("Explore");
		else if(Input.GetKeyDown(KeyCode.J))
		{
			PlayEvent("Dungeon");
		}
		else if(Input.GetKeyDown(KeyCode.K))
		{
			PlayEvent("Silence");
		}
		else if(Input.GetKeyDown(KeyCode.L))
		{
			PlayEvent("Library");
		}
		else if(Input.GetKeyDown(KeyCode.M))
		{
			PlayEvent("Head");
		}
		
		
		
	}
}

public enum SoundType
{
    Music, Effects 
}
