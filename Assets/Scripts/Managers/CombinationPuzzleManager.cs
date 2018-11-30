using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPuzzleManager : MonoBehaviour
{

    public string PuzzleId;
	public List<CombinationPuzzleObject> PuzzlePiece;
	public Pickup Reward;
	public Door DoorToBeOpened;
	public PuzzleType TypeOfPuzzle;
	
	

	private int _currentIndex;

	public void Selection(CombinationPuzzleObject guess)
	{
		Debug.Log(PuzzlePiece.IndexOf(guess)+" "+_currentIndex+ " "+ PuzzlePiece.Count );
		if (PuzzlePiece.IndexOf(guess) == _currentIndex)
		{
			_currentIndex++;
			if (_currentIndex  == PuzzlePiece.Count)
				PuzzleSolved();
		}
		else
		{
			_currentIndex = 0;
		}
		Debug.Log(_currentIndex);
	}

	private void PuzzleSolved()
	{
		foreach (CombinationPuzzleObject item in PuzzlePiece)
		{
			item.setSolved(true);
			
		}
		if(TypeOfPuzzle == PuzzleType.ItemReward)
			Reward.Interact();
		else if(TypeOfPuzzle == PuzzleType.OpenDoor)
			DoorToBeOpened.Interact();
		
		EventManager.TriggerEvent("PuzzleSolved" + PuzzleId);

	}
}
public enum PuzzleType
   
{
	ItemReward, OpenDoor
}
