using System.Collections;
using Objects;
using UnityEngine;

public class MusicPlayer : InGameObject {

	public string[] Tracks;
	public float PauseTime;

	private bool _isPlaying;
	public override void Interact()
	{
		if(!_isPlaying) StartCoroutine(PlayTracks());
		
	}

	private IEnumerator PlayTracks()
	{
		_isPlaying = true;
		foreach (string track in Tracks)
		{
			AudioManager.Instance.PlayEvent(track);
			yield return new WaitForSeconds(PauseTime);
		}

		_isPlaying = false;

	}
}
