using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractiveObject
{

	public TextAsset TextFile;
	public string NPCName;

	public bool IsInteractable()
	{
		return true;
	}

	public void Interact(Player player)
	{
		TextBoxManager.Instance.LoadScript(TextFile, NPCName);
		TextBoxManager.Instance.EnableTextBox();
	}
}
