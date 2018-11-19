using System.Linq;
using UnityEngine;

/// <summary>
/// The Sound class contains the AudioClip and the AudioSource playing it.
/// It allows to set an AudioSource for the AudioClip and play it.
/// </summary>
[System.Serializable]
public class Sound
{

	public string Name;
	public AudioClip Clip;

	[Range(0f, 1f)] public float Volume = 0.5f;
	[Range(0.5f, 1.5f)] public float Pitch = 1f;

	[Range(0f, 0.5f)] public float VolumeRange = 0.1f;
	[Range(0f, 0.5f)] public float PitchRange = 0.1f;
	
	private AudioSource _source;

	/// <summary>
	/// Used to set the AudioSource of the Sound.
	/// </summary>
	/// <param name="source">The desired AudioSource</param>
	public void SetSource(AudioSource source)
	{
		_source = source;
		_source.clip = Clip;
	}

	/// <summary>
	/// Play the Sound on the Sound's AudioSource
	/// </summary>
	public void Play()
	{
		_source.volume = Volume * (1 + Random.Range(-VolumeRange / 2, VolumeRange / 2));
		_source.pitch = Pitch * (1 + Random.Range(-PitchRange / 2, PitchRange / 2));
		_source.Play();
	}

}

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

	[SerializeField, Header("Sounds")] public Sound[] Sounds;
	
	private bool _sfxOn = true;

	private void Start()
	{
		for (int i = 0; i < Sounds.Length; i++) {
			GameObject go = new GameObject("Sound_" + i + "_" + Sounds[i].Name);
			go.transform.SetParent(this.transform);
			Sounds[i].SetSource(go.AddComponent<AudioSource>());
		}
	}

	/// <summary>
	/// Method used to play a specific sound.
	/// </summary>
	/// <param name="soundName">The name of the sound to be played.</param>
	public void PlaySound(string soundName)
	{
		if (!_sfxOn) return;
		
		for (int i = 0; i < Sounds.Length; i++) {
			if (Sounds[i].Name == soundName)
			{
				Sounds[i].Play();
				return;
			}
		}
		
		// Sound not found
		Debug.Log("AudioManager: Sound \""+ soundName + "\"not found in Sounds array");
		
	}

	public void EnableSFX()
	{
		_sfxOn = true;
	}

	public void DisableSFX()
	{
		_sfxOn = false;
	}
	
}
