using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utility;

public class FollowPathScript : MonoBehaviour
{

	public Path PathToFollow;
	public float Speed;
	public bool AlwaysEnabled;
	
	
	private bool _canFollow;
	private bool _hasFinished;
	private int _position;
	private bool _isInDialogue;


	void Start()
	{
		_canFollow = AlwaysEnabled;
	}


	// Update is called once per frame
	void Update()
	{


		if (_canFollow)
		{
			
			transform.position = Vector3.MoveTowards(transform.position, PathToFollow.WayPoints[_position].transform.position, Speed*Time.deltaTime);
			if (transform.position == PathToFollow.WayPoints[_position].transform.position)
			{
				if (PathToFollow.WayPoints[_position].Type == PathNodeType.Dialogue )
				{
					if (!_isInDialogue && !TextBoxManager.Instance.IsActive)
					{
						_isInDialogue = true;
						TextBoxManager.Instance.LoadDialogue(PathToFollow.WayPoints[_position].Text);
					}
					else if(_isInDialogue && !TextBoxManager.Instance.IsActive)
					{
						_position++;
						_isInDialogue = false ;
					}
				}
				else
				{
					_position++;	
				}
				

				if (_position == PathToFollow.WayPoints.Count )
				{
					if (PathToFollow.Type == PathType.Linear)

					{
						_canFollow = false;
						_hasFinished = true;
					}
					
					else
					{
						_position = 0;
					}
				}
			
			}
		}
		
	}

	public void StartPath()
	{
		_canFollow = true;
	}
	public void EndPath()
	{
		_canFollow = false;
	}
	public bool canFollow()
	{
		return _canFollow;
	}

	public bool isPathCompleted()
	{
		return _hasFinished;
	}

	public bool IsInDialogue()
	{
		return _isInDialogue;
	}
}

