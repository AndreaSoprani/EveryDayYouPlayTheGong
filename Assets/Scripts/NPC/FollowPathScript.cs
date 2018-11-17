using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FollowPathScript : MonoBehaviour
{

	public Path PathToFollow;
	public float Speed;
	public bool AlwaysEnabled;
	
	
	private bool _canFollow;
	private bool _hasFinished;
	private int _position;


	void Start()
	{
		_canFollow = AlwaysEnabled;
	}


	// Update is called once per frame
	void Update()
	{


		if (_canFollow)
		{
			
			transform.position = Vector3.MoveTowards(transform.position, PathToFollow.WayPoints[_position].position, Speed*Time.deltaTime);
			if (transform.position == PathToFollow.WayPoints[_position].position)
			{
				_position++;
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
}

