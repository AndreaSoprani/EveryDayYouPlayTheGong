using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface used for objects that can be interacted with or played.
/// </summary>
public interface IPlayInteractObject
{

	/// <summary>
	/// Used to check if the object can be interacted with.
	/// </summary>
	/// <returns>true if the object can be interacted with, false otherwise</returns>
	bool IsInteractable();
	
	/// <summary>
	/// Used to check if the object can be played.
	/// </summary>
	/// <returns>true if the object can be played, false otherwise</returns>
	bool IsPlayable();
	
	/// <summary>
	/// Used to interact with the object.
	/// </summary>
	void Interact();
	
	/// <summary>
	/// Used to play the object.
	/// </summary>
	void Play();

}
