using UnityEngine;

namespace Utility
{
    /// <summary>
    /// The Sound class contains the AudioClip and the AudioSource playing it.
    /// It allows to set an AudioSource for the AudioClip and play it.
    /// </summary>
    [CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
    public class Sound : ScriptableObject
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
        /// Check if the Sound has already an AudioSource
        /// </summary>
        /// <returns>false if the AudioSource is null, true otherwise.</returns>
        public bool HasSource()
        {
            return _source != null;
        }

        /// <summary>
        /// Play the Sound on the Sound's AudioSource
        /// </summary>
        public void Play(float SFXVolume)
        {
            _source.volume = Volume * SFXVolume * (1 + Random.Range(-VolumeRange / 2, VolumeRange / 2));
            _source.pitch = Pitch * (1 + Random.Range(-PitchRange / 2, PitchRange / 2));
            _source.Play();
        }

    }
}