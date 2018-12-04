using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class PlayingArea : MonoBehaviour {
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		InGameObject inGameObject = other.gameObject.GetComponent<InGameObject>();
		
		if (inGameObject != null)
		{
			Player.Instance.SetPlayableFound(inGameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		InGameObject inGameObject = other.gameObject.GetComponent<InGameObject>();
		
		if (inGameObject != null)
		{
			Player.Instance.RemovePlayableFound(inGameObject);
		}
	}
}
