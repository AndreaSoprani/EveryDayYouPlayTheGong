﻿using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class CombinationPuzzleObject : Instrument
{

	private bool _solved = false;

	public override bool IsPlayable()
	{
		return true;
	}

	public override void Play()
	{
		if(!_solved)
		{
				GetComponentInParent<CombinationPuzzleManager>().Selection(this);	
		}
		
		base.Play();
	}

	public void setSolved(bool b)
	{
		_solved = b;
	}
}
