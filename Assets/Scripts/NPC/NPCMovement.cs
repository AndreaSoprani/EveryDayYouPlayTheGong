using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Boo.Lang;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMovement : MonoBehaviour
{
	public string Id;
	[Header("Movement")] 
	public float Velocity = 1f;
	public float MaximumRadiusX = 5f;
	public float MaximumRadiusY = 5f;
	public float Step = 1;
	private Vector3 _center;
	public float WaitTime = 2f;
	public float ArrivalDistance = 0.1f;
	public Color GizmoColor=Color.red;

	
	[Header("Collisions")] 
	public RayCastPositionsHd RayCastPositions;
	public LayerMask ObstacleLayer;

	
	private Vector3 _endPosition;
	private Vector3 _direction;

	private Animator _animator;
	private bool _walk = true;
	private Vector3 _facing;
	private bool _inDialogue;
	private bool _isFollowingPath;
	

	private void OnDrawGizmos()
	{
		
		Gizmos.color=GizmoColor;
		Gizmos.DrawLine(new Vector3(transform.position.x-MaximumRadiusX,transform.position.y+MaximumRadiusY),new Vector3(transform.position.x+MaximumRadiusX,transform.position.y+MaximumRadiusY));
		Gizmos.DrawLine(new Vector3(transform.position.x+MaximumRadiusX,transform.position.y+MaximumRadiusY),new Vector3(transform.position.x+MaximumRadiusX,transform.position.y-MaximumRadiusY));
		Gizmos.DrawLine(new Vector3(transform.position.x+MaximumRadiusX,transform.position.y-MaximumRadiusY),new Vector3(transform.position.x-MaximumRadiusX,transform.position.y-MaximumRadiusY));
		Gizmos.DrawLine(new Vector3(transform.position.x-MaximumRadiusX,transform.position.y-MaximumRadiusY),new Vector3(transform.position.x-MaximumRadiusX,transform.position.y+MaximumRadiusY));
	}

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		_endPosition = _center = transform.position;
		switch (_animator.GetInteger("Direction"))
		{
			case 0: 
				_facing = Vector3.up;
				break;
			case 1:
				_facing = Vector3.right;
				break;
			case 3:
				_facing = Vector3.left;
				break;
			default:
				_facing = Vector3.down;
				break;
		}
		
		_inDialogue = false;
		EventManager.StartListening("NPCEnterDialogue"+Id, EnterDialogue);
		
		
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
				
				ChangeFacing(_direction);
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
		Collection<Vector3> movement = RayCastPositions.Vector3ToRayCastPositionHd(delta);
		
		for (int i = 0; i < movement.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(movement[i], delta, delta.magnitude , ObstacleLayer);
			
			if (hit.collider != null) return false;
		}

		Vector3 finalPosition = transform.position + delta;
		
		return !(finalPosition.x > _center.x + MaximumRadiusX || finalPosition.x < _center.x - MaximumRadiusX ||
		       finalPosition.y > _center.y + MaximumRadiusY ||
		       finalPosition.y < _center.y - MaximumRadiusY);


	}
	
	/// <summary>
	/// Picks a new destination inside a circle that has its center in the original position of the NPC
	/// </summary>
	void PickNewDestination()
	{
		bool axis = Random.Range(0, 10) < 5;

		if (axis) // x
		{
			_endPosition = new Vector3(_center.x + Random.Range(-MaximumRadiusX, + MaximumRadiusX), transform.position.y, 0);
		}
		else // y
		{
			_endPosition = new Vector3(transform.position.x, _center.y + Random.Range(-MaximumRadiusY, + MaximumRadiusY), 0);
		}

		_direction = (_endPosition - transform.position).normalized;
		
		
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

		_facing = direction;
		
		
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
		
		//ChangeFacing(Player.Instance.transform.position - transform.position);
		_animator.SetBool("Walking", false);
		
		EventManager.StopListening("NPCEnterDialogue"+Id, EnterDialogue);
		EventManager.StartListening("NPCExitDialogue"+Id, ExitDialogue);
	}

	/**
	 * Used to exit dialogue mode.
	 * Movement is re-enabled.
	 */
	public void ExitDialogue()
	{
		_inDialogue = false;
		
		//ChangeFacing(_direction);
		_animator.SetBool("Walking", _walk);
		
		EventManager.StopListening("NPCExitDialogue"+Id, ExitDialogue);
		EventManager.StartListening("NPCEnterDialogue"+Id, EnterDialogue);
	}
}
