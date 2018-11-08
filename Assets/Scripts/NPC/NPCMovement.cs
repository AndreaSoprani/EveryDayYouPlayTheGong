using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMovement : MonoBehaviour
{
	[Header("Movement")] 
	public float Velocity = 1f;
	public float Radius = 5f;
	public float WaitTime = 1f;
	public float ArrivalDistance = 1f;
	
	[Header("Collisions")] 
	public RayCastPositions RayCastPositions;
	public LayerMask ObstacleLayer;

	private Vector3 _center;
	private Vector3 _endPosition;
	private Vector3 _direction;

	private bool _walk = true;
	
	// Use this for initialization
	void Start ()
	{
		_center  = transform.position;
		PickNewDestination();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_walk)
		{
			Vector3 deltaMovement = _direction * Velocity * Time.deltaTime;
			
			if (!HasArrived() && CanMove(deltaMovement))
			{
				transform.position = transform.position + deltaMovement;
			}
			else
			{
				_walk = false;
				StartCoroutine(Wait());
				PickNewDestination();
			}
		}
	}

	/// <summary>
	/// Checks if the NPC is near the destination.
	/// </summary>
	/// <returns>True if the NPC is near, false otherwise.</returns>
	private bool HasArrived()
	{
		float distance = Vector3.Distance(transform.position, _endPosition);

		return distance <= ArrivalDistance;
	}

	/// <summary>
	/// Checks if the NPC can move in a specific direction without colliding with an obstacle
	/// </summary>
	/// <param name="delta">The amount of displacement from the initial position</param>
	/// <returns>True if the NPC can move in the direction set, false otherwise.</returns>
	private bool CanMove(Vector3 delta)
	{
		Vector3 vertical = Vector3.zero;
		Vector3 horizontal = Vector3.zero;

		if (delta.x > 0)
		{
			horizontal = RayCastPositions.Right.position;
		}
		else if (delta.x < 0)
		{
			horizontal = RayCastPositions.Left.position;
		}
		
		if (delta.y > 0)
		{
			vertical = RayCastPositions.Up.position;
		}
		else if (delta.y < 0)
		{
			vertical = RayCastPositions.Down.position;
		}
		
		RaycastHit2D hitX = Physics2D.Raycast(horizontal, transform.right * Math.Sign(delta.x), delta.x, ObstacleLayer);
		RaycastHit2D hitY = Physics2D.Raycast(vertical, transform.up * Math.Sign(delta.y), delta.y, ObstacleLayer);

		return hitX.collider == null || hitY.collider == null;
	}
	
	/// <summary>
	/// Picks a new destination inside a circle that has its center in the original position of the NPC
	/// </summary>
	void PickNewDestination()
	{
		_endPosition = (Vector3) Random.insideUnitCircle * Radius + _center;

		_direction = _endPosition - transform.position;
		_direction.Normalize();
	}

	/// <summary>
	/// Stops the movement of the NPC before starting to walk again.
	/// </summary>
	/// <returns></returns>
	IEnumerator Wait()
	{
		//TODO Start idle animation
		
		yield return new WaitForSeconds(WaitTime);

		_walk = true;
	}
}
