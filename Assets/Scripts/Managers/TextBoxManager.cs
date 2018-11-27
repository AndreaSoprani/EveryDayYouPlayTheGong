using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Utility;

public class TextBoxManager : MonoBehaviour
{

	private static TextBoxManager _instance;
	public static TextBoxManager Instance { get { return _instance; } }

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

	public GameObject TextBox;

	public Text TextBoxText;
	public Text TextBoxName;

	
	
	private string[] _textLines;

	private int _currentLine;

	public bool IsActive;
	public bool StopPlayerMovementOnDialogue = true;

	// Use this for initialization
	void Start ()
	{
		
		IsActive = false;

	}

	private void Update()
	{

		if (!IsActive) return;
		
		// Visualize text
		TextBoxText.text = _textLines[_currentLine];
		
		// Check for continue
		if (Input.GetKeyDown(KeyCode.Return))
		{
			_currentLine++;
		}

		if (_currentLine >= _textLines.Length) DisableTextBox();
		
	}

	/// <summary>
	/// Enables the text box and starts the dialogue at the current position.
	/// TextBox can only be enabled if a text file is loaded.
	/// </summary>
	public void EnableTextBox()
	{
		if (_textLines.Length == 0) return;
		TextBox.SetActive(true);
		IsActive = true;
		if(StopPlayerMovementOnDialogue) Player.Instance.EnterDialogue();
	}

	/// <summary>
	/// Disables text box and stops the dialogue at the current position.
	/// </summary>
	private void DisableTextBox()
	{
		TextBox.SetActive(false);
		IsActive = false;
		Player.Instance.ExitDialogue();
	}

	/// <summary>
	/// Used to load a dialogue.
	/// </summary>
	/// <param name="dialogue">The dialogue to read from.</param>
	public void LoadDialogue(Dialogue dialogue)
	{
		
		// Set the text lines.
		if (dialogue.TextAsset != null)
		{
			_textLines = dialogue.TextAsset.text.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);
		}

		// Set the NPC name.
		TextBoxName.text = dialogue.NPCName;

		// Activate all quests to activate
		for (int i = 0; i < dialogue.QuestsToActivate.Count; i++)
		{
			QuestManager.Instance.ActivateQuest(dialogue.QuestsToActivate[i].QuestID);
		}

		_currentLine = 0;
	}
	
}
