using System.Collections;
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
	

	private int _currentIndex;
	private float _timePassed;
	

	private void Update()
	{
		if (HasTimeout)
			_timePassed += Time.deltaTime;
	}

	public void Selection(CombinationPuzzleObject guess)
	{
		Debug.Log("Guess: " + guess.name + " Current: " + PuzzlePiece[_currentIndex].name + " Total: "+ PuzzlePiece.Count );
		
		if (PuzzlePiece[_currentIndex] == guess) // In sequence
		{
			if (_currentIndex == 0)
				_timePassed = 0;
			if(HasTimeout) Debug.Log("Time Passed: "+_timePassed+ "Timeout: "+TimeoutInSeconds);
			
			_currentIndex++;
			
			if (_currentIndex == PuzzlePiece.Count && _timePassed <= TimeoutInSeconds)
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
			DoorToBeOpened.Interact();
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
		EventManager.TriggerEvent("PuzzleSolved" + PuzzleId);

	}
}
public enum PuzzleType
   
{
	ItemReward, OpenDoor, Dialogue
}
