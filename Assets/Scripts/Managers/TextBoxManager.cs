using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using TMPro;
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

	public GameObject DialogueBox;

	private TextMeshProUGUI _nametext; // The GUI Text element used to display the name of the NPC.
	private TextMeshProUGUI _dialoguetext; // The GUI Text element used to display the dialogue.
	
	private string[] _textLines;

	private int _currentLine;

	private bool _dontChangeNPCFacing;
	
	private List<Quest> _questsToActivate;
	private List<Item> _itemsToAdd;
	private List<Item> _itemsToRemove;
	private List<GameObject> _gameObjectToDestroy;

	private bool _progressionEnabled;
	
	public bool IsActive;

	// Use this for initialization
	void Start ()
	{
		
		IsActive = false;
		_questsToActivate = new List<Quest>();
		_itemsToAdd = new List<Item>();
		_itemsToRemove = new List<Item>();
		_gameObjectToDestroy=new List<GameObject>();

		TextMeshProUGUI[] texts = DialogueBox.GetComponentsInChildren<TextMeshProUGUI>(true);

		foreach (TextMeshProUGUI text in texts)
		{
			if (text.name == "Name") _nametext = text;
			else if (text.name == "Dialogue") _dialoguetext = text;
		}

	}

	private void LateUpdate()
	{

		if (!IsActive) return;
		
		// Visualize text
		_dialoguetext.text = _textLines[_currentLine];
		
		// Check for continue
		if (Input.GetKeyDown(KeyCode.X) && _progressionEnabled)
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
		DialogueBox.SetActive(true);
		StartCoroutine(DisableProgression());
		IsActive = true;
		EventManager.TriggerEvent("EnterDialogue");
		if(!_dontChangeNPCFacing) EventManager.TriggerEvent("NPCEnterDialogue");
	}

	/// <summary>
	/// Disables text box and stops the dialogue at the current position.
	/// </summary>
	private void DisableTextBox()
	{
		// Disable text box.
		DialogueBox.SetActive(false);
		IsActive = false;
		
		// Signal dialogue exit
		EventManager.TriggerEvent("ExitDialogue");
		if(!_dontChangeNPCFacing) EventManager.TriggerEvent("NPCExitDialogue");
		
		EndDialogueActivations();
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
		_nametext.text = dialogue.NPCName;

		_dontChangeNPCFacing = dialogue.DontChangeNPCFacing;

		_questsToActivate = dialogue.QuestsToActivate;
		_itemsToAdd = dialogue.ItemsToAdd;
		_itemsToRemove = dialogue.ItemsToRemove;

		_currentLine = 0;
	}

	/// <summary>
	/// Used to activate all quests, give all items and remove others.
	/// </summary>
	private void EndDialogueActivations()
	{
		// Add all items to add
		for (int i = 0; i < _itemsToRemove.Count; i++)
		{
			Player.Instance.RemoveItem(_itemsToRemove[i]);
		}
		
		// Add all items to add
		for (int i = 0; i < _itemsToAdd.Count; i++)
		{
			Player.Instance.AddItem(_itemsToAdd[i]);
		}
		
		// Activate all quests to activate
		for (int i = 0; i < _questsToActivate.Count; i++)
		{
			QuestManager.Instance.ActivateQuest(_questsToActivate[i]);
		}

		foreach (GameObject g in _gameObjectToDestroy)
		{
			Destroy(g);
		}
	}

	private IEnumerator DisableProgression()
	{
		_progressionEnabled = false;
		yield return new WaitForSeconds(Time.deltaTime);
		_progressionEnabled = true;
	}
	
}
