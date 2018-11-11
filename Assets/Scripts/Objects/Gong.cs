using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gong : MonoBehaviour, IPlayableObject, IInteractiveObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	 * PLAY
	 */

	public bool IsPlayable()
	{
		return true;
	}

	public void Play()
	{
		Debug.Log("GOOOOOOOOOOOOOONG");
		//TODO implement.
	}

	
	/*
	 * INTERACT
	 */
	
	public bool IsInteractable()
	{
		return true;
	}

	public void Interact()
	{
		Debug.Log("It's a very ancient gong, it should not be played.");
		//TODO implement.
	}
}
