using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPuzzleObject : MonoBehaviour, IPlayableObject
{

	private bool _solved;

	public bool IsPlayable()
	{
		return !_solved;
	}

	public void Play()
	{
		if(!_solved)
		{
			
				GetComponentInParent<CombinationPuzzleManager>().Selection(this);
			
		}
	}

	public void setSolved(bool b)
	{
		_solved = b;
		
	}
}
