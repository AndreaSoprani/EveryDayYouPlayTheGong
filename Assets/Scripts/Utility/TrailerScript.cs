using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerScript : MonoBehaviour
{
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.J))
			AudioManager.Instance.PlayMusic("Dungeon");
		if(Input.GetKeyDown(KeyCode.Keypad1))
			DayProgressionManager.Instance.Progress();
		
	}
}
