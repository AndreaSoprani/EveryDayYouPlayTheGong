using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowController : MonoBehaviour
{

	
	public FollowPathScript FollowPath;
	public float LagSeconds =  0.5f;
	public bool FollowOnce;
	
	

	private Vector3[] _positionBuffer;
	private float[] _timeBuffer;
	private int _oldestIndex;
	private int _newestIndex;

	
	
	void Start() {
		int bufferLength = Mathf.CeilToInt(LagSeconds * 60);
		_positionBuffer = new Vector3[bufferLength];
		_timeBuffer = new float[bufferLength];

		
		_positionBuffer[1] = transform.position;
		_timeBuffer[0] = _timeBuffer[1] = Time.time;

		_oldestIndex = 0;
		_newestIndex = 1;
	}


	void LateUpdate () {
		if (FollowPath.canFollow())
		{
			// Insert newest position into our cache.
			// If the cache is full, overwrite the latest sample.
			int newIndex = (_newestIndex + 1) % _positionBuffer.Length;
			if (newIndex != _oldestIndex)
				_newestIndex = newIndex;

			_positionBuffer[_newestIndex] = transform.position;
			_timeBuffer[_newestIndex] = Time.time;

			// Skip ahead in the buffer to the segment containing our target time.
			float targetTime = Time.time - LagSeconds;
			int nextIndex;
			while (_timeBuffer[nextIndex = (_oldestIndex + 1) % _timeBuffer.Length] < targetTime)
				_oldestIndex = nextIndex;

			// Interpolate between the two samples on either side of our target time.
			float span = _timeBuffer[nextIndex] - _timeBuffer[_oldestIndex];
			float progress = 0f;
			if (span > 0f)
			{
				progress = (targetTime - _timeBuffer[_oldestIndex]) / span;
			}

			Player.Instance.transform.position = Vector3.MoveTowards(_positionBuffer[_oldestIndex], _positionBuffer[nextIndex], progress);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (!FollowPath.canFollow() && !FollowPath.isPathCompleted() )
			{
				_positionBuffer[0] = Player.Instance.transform.position;
				Debug.Log("Start Follow");
				FollowPath.StartPath();

			}
			else
			{
				if (FollowPath.isPathCompleted() && FollowOnce)
				{
					GetComponent<BoxCollider2D>().enabled = false;
					Debug.Log("End Follow");
				}
			}
			
			
		}
	}
}
