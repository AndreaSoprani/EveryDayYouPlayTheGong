using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class CombinationPuzzleObject : InGameObject
{

	private bool _solved;

	public override bool IsPlayable()
	{
		return !_solved;
	}

	public override void Play()
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
