using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

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

	public Player Player;
	
	private string[] _textLines;

	private int _currentLine;
	private int _endAtLine;

	public bool IsActive;
	public bool StopPlayerMovementOnDialogue = true;

	// Use this for initialization
	void Start ()
	{
		Debug.Log("START");
		Player = FindObjectOfType<Player>();
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

		if (_currentLine > _endAtLine) DisableTextBox();
		
	}

	/**
	 * Enables the text box and starts the dialogue at the current position.
	 * TextBox can only be enabled if a text file is loaded.
	 */
	public void EnableTextBox()
	{
		if (_textLines.Length == 0) return;
		TextBox.SetActive(true);
		IsActive = true;
		if(StopPlayerMovementOnDialogue) Player.EnterDialogue();
	}

	/**
	 * Disables text box and stops the dialogue at the current position.
	 */
	private void DisableTextBox()
	{
		TextBox.SetActive(false);
		IsActive = false;
		Player.ExitDialogue();
	}

	/**
	 * Used to load a text script.
	 * Standard startLine = 0.
	 * Standard endAtLine = -1 (end of document).
	 */
	public void LoadScript(TextAsset newTextFile, string npcName, int startLine = 0, int endAtLine = -1)
	{
		if (newTextFile != null)
		{
			_textLines = newTextFile.text.Split('\n');
		}

		TextBoxName.text = npcName;

		_currentLine = startLine < _textLines.Length && startLine >= 0 ? startLine : 0;
		_endAtLine = endAtLine < _textLines.Length && endAtLine >= 0 ? endAtLine : (_textLines.Length - 1);
	}

	public void LoadScript(string text, string npcName)
	{
		_textLines = new string[1];
		_textLines[0] = text;

		TextBoxName.text = npcName;

		_currentLine = 0;
		_endAtLine = 0;

	}
	
	
}
