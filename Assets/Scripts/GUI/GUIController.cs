using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	[Range(1,60)]
    public int TimeDuration = 1;
	public Slider ProgressBar;
	public GameObject PauseMenu;
	public GameObject Inventory;
	
    private float _timePassed;
    private float _timeSlot;
	private bool _isGamePaused;
	
	void Start()
	{
	    _timeSlot=TimeDuration*60;
	}

	private void OnEnable()
	{
		EventManager.StartListening("InteractionGUIElement", DisplayInteraction);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_isGamePaused)
			{
				
				Resume();
			}
			else
			{
				
				Pause();
			}
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			Inventory.SetActive(true);
		}
		
		if (!_isGamePaused)
		{
			_timePassed += Time.deltaTime;
			if (_timePassed > _timeSlot)
				_timePassed = 0;
		
			ProgressBar.value=CalculateProgress();
		}
		
	}

	private void Pause()
	{
		Time.timeScale = 0;
		_isGamePaused = true;
		PauseMenu.SetActive(true);
	}

	public void Resume()
	{
		Time.timeScale = 1;
		_isGamePaused = false;
		PauseMenu.SetActive(false);
	}


	float CalculateProgress()
	{
		return _timePassed / _timeSlot;
	}

	/// <summary>
	/// Used to display the option to interact with an object on screen.
	/// </summary>
	void DisplayInteraction()
	{
		Debug.Log("Press KEY to interact."); 
		//TODO: Retrieve correct key.
		//TODO: Display it on the screen.
	}

	public void QuitGame()
	{
		Debug.Log("NOPEEEE");
	}
	
	public void LoadGame()
	{
		Debug.Log("Load");
	}
	public void Options()
	{
		Debug.Log("Options");
	}
	
}
