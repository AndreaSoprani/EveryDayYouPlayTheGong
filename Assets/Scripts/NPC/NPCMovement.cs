using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

	private Animator _animator;
	private bool _walk = true;
	private bool _inDialogue;
	
	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		_center  = transform.position;
		PickNewDestination();

		_inDialogue = false;
		EventManager.StartListening("EnterDialogue", EnterDialogue);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_inDialogue) return;
		
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
		Collection<Vector3> movement = RayCastPositions.Vector3ToRayCastPosition(delta);
		
		for (int i = 0; i < movement.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(movement[i], delta, delta.magnitude , ObstacleLayer);
			
			if (hit.collider != null) return false;
		}

		return true;

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
	/// Changes the sprites of the NPC based on the direction
	/// </summary>
	/// <param name="direction">Direction the NPC should face</param>
	private void ChangeFacing(Vector3 direction)
	{
		float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
		
		if (angle > -45f && angle < 45f)
		{
			_animator.SetInteger("Direction", 1);
		}
		else if (angle >= 45f && angle <= 135f)
		{
			_animator.SetInteger("Direction", 0);
		}
		else if (angle > 135f || angle < -135f)
		{
			_animator.SetInteger("Direction", 3);
		}
		else
		{
			_animator.SetInteger("Direction", 2);
		}
	}
	
	/// <summary>
	/// Stops the movement of the NPC before starting to walk again.
	/// </summary>
	/// <returns></returns>
	IEnumerator Wait()
	{
		_animator.SetBool("Walking", false);
		
		yield return new WaitForSeconds(WaitTime);

		while (_inDialogue)
			yield return null;
		
		PickNewDestination();
		ChangeFacing(_direction);
		_walk = true;
		
		_animator.SetBool("Walking", true);
	}
	
	/**
	 * Used to enter in dialogue mode.
	 * Movement is disabled.
	 */
	public void EnterDialogue()
	{
		_inDialogue = true;
		
		ChangeFacing(Player.Instance.transform.position - transform.position);
		_animator.SetBool("Walking", false);
		
		EventManager.StopListening("EnterDialogue", EnterDialogue);
		EventManager.StartListening("ExitDialogue", ExitDialogue);
	}

	/**
	 * Used to exit dialogue mode.
	 * Movement is re-enabled.
	 */
	public void ExitDialogue()
	{
		_inDialogue = false;
		
		ChangeFacing(_direction);
		_animator.SetBool("Walking", _walk);
		
		EventManager.StopListening("ExitDialogue", ExitDialogue);
		EventManager.StartListening("EnterDialogue", EnterDialogue);
	}
}
