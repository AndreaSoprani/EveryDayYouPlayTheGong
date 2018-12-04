using System.Linq;
using UnityEngine;
using Utility;

public class AudioManager : MonoBehaviour
{

	private static AudioManager _instance;
	public static AudioManager Instance { get { return _instance; } }
	private uint _bankID;
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
		AkSoundEngine.LoadBank("Main", AkSoundEngine.AK_DEFAULT_POOL_ID, out _bankID);
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

	//HERE JUST FOR TEST!!!!!!!!!!!!!!!!!!!!! PLEASE DELETE THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.J))
			PlayEvent("Explore");
		if(Input.GetKeyDown(KeyCode.K))
			StopEvent("Explore",1);
		if (Input.GetKeyDown(KeyCode.L))
		{
			ResumeEvent("Explore",1);
		}
		if(Input.GetKeyDown(KeyCode.H))
		
			PauseEvent("Explore",1);
	}
}
