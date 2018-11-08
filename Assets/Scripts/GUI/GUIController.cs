using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	[Range(1,60)]
    public int TimeDuration = 1;
	public Slider ProgressBar;
	
    private float _timePassed=0f;
    private float _timeSlot;
	
	void Start()
	{
	    _timeSlot=TimeDuration*60;
	} 
	// Update is called once per frame
	void Update ()
	{
		_timePassed += Time.deltaTime;
		if (_timePassed > _timeSlot)
			_timePassed = 0;
		
		ProgressBar.value=CalculateProgress();
		

	}

	float CalculateProgress()
	{
		return _timePassed / _timeSlot;
	}

	void DisplayInteractionOption(KeyCode keyCode)
	{
		Debug.Log("Press " + keyCode.ToString() + " to interact."); //TODO: Display it on the screen.
	}
	
}
