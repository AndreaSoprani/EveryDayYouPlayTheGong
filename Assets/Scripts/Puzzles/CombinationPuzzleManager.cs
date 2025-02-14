﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class CombinationPuzzleManager : MonoBehaviour
{

    public string PuzzleId;
	public List<CombinationPuzzleObject> PuzzlePiece;
	public bool HasTimeout;
	public Pickup Reward;
	public Door DoorToBeOpened;
	public PuzzleType TypeOfPuzzle;
	public float TimeoutInSeconds;
	public NPCInteractable Target;
	public bool InstantTrigger;
	public Dialogue DialogueUnlocked;
	public bool AlwaysEnable;
	public bool Silent;
	

	private int _currentIndex;
	private float _timePassed;
	private bool _solved = false;
	

	private void Update()
	{
		if (HasTimeout)
		{
			_timePassed += Time.deltaTime;
			if (_timePassed > TimeoutInSeconds && _currentIndex != 0 && !_solved)
			{
				_currentIndex = 0;
				AudioManager.Instance.PlayEvent("StartClock");
			}
		}
		
	}

	public void Selection(CombinationPuzzleObject guess)
	{
		Debug.Log("Guess: " + guess.name + " Current: " + PuzzlePiece[_currentIndex].name + " Total: "+ PuzzlePiece.Count );
		
		if (PuzzlePiece[_currentIndex] == guess) // In sequence
		{
			Debug.Log("OK");
			if (_currentIndex == 0 && HasTimeout)
			{
				_timePassed = 0;
				AudioManager.Instance.PlayEvent("StartClock");
			}
			if(HasTimeout) Debug.Log("Time Passed: "+_timePassed+ "Timeout: "+TimeoutInSeconds);
			
			_currentIndex++;
			
			if (_currentIndex == PuzzlePiece.Count)
			{
				PuzzleSolved();
			}
		} 
		else if (guess == PuzzlePiece[0]) // First piece two times (Instead of first and then second)
		{
			_currentIndex = 1;
			_timePassed = 0;
			
			if(HasTimeout) Debug.Log("Time Passed: "+_timePassed+ "Timeout: "+TimeoutInSeconds);
			
			if (_currentIndex == PuzzlePiece.Count && _timePassed <= TimeoutInSeconds)
			{
				PuzzleSolved();
			}
		} 
		else // Wrong piece
		{
			_currentIndex = 0;
		}
		Debug.Log(_currentIndex);
	}

	private void PuzzleSolved()
	{
		_solved = true;
		
		if(!AlwaysEnable)
		{
			foreach (CombinationPuzzleObject item in PuzzlePiece)
			{
				item.setSolved(true);
			}
		}
		if(TypeOfPuzzle == PuzzleType.ItemReward)
			Reward.Interact();
		else if(TypeOfPuzzle == PuzzleType.OpenDoor)
			DoorToBeOpened.OpenDoor();
		else if (TypeOfPuzzle == PuzzleType.Dialogue)
		{
			if (InstantTrigger)
			{
				Target.InstantDialogue(DialogueUnlocked);
			}
			else
			{
				Target.Dialogues.Add(DialogueUnlocked);
			}
			
		}
		
		_currentIndex = 0;
		if(!Silent) AudioManager.Instance.PlayEvent("PuzzleFinish");
		EventManager.TriggerEvent("PuzzleSolved" + PuzzleId);

	}

	public bool IsSolved()
	{
		return _solved;
	}
}

public enum PuzzleType
   
{
	ItemReward, OpenDoor, Dialogue
}
