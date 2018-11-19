using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gong : MonoBehaviour, IPlayableObject, IInteractiveObject
{

	public TextAsset InteractionMessage;
	public string GongName;

	/*
	 * PLAY
	 */

	public bool IsPlayable()
	{
		return true;
	}

	public void Play()
	{
		AudioManager.Instance.PlaySound("GongSoundTest");
	}

	
	/*
	 * INTERACT
	 */
	
	public bool IsInteractable()
	{
		return true;
	}

	public void Interact(Player player)
	{
		TextBoxManager.Instance.LoadScript(InteractionMessage, GongName);
		TextBoxManager.Instance.EnableTextBox();
	}
}
