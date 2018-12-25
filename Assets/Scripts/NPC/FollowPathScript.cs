using System.Collections;
using System.Collections.ObjectModel;
using DefaultNamespace;
using Objects;
using UnityEngine;
using Utility;

public class FollowPathScript : InGameObject
{
	[Header("Movement")] 
	public float Speed = 1f;
	public Path PathToFollow;
	
	[Header("Collisions")] 
	public RayCastPositions RayCastPositions;
	public LayerMask ObstacleLayer;

	private Vector3 _center;
	private Vector3 _endPosition;
	private Vector3 _direction;

	private Animator _animator;
	private bool _walk = true;
	private bool _dialogueFinished;
	private bool _isWaiting;
	private bool _isFollowingPath;
	private int _position;
	private Dialogue _newDialogue;
	private bool hasFinished=false;
	private bool _inDialogue = false;
	
	
	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		
		
		_isWaiting = _newDialogue!=null;
		_dialogueFinished = false;
		
		
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		
		
		if (!_isWaiting && !_inDialogue)
		{
			Vector3 nextPosition = Vector3.MoveTowards(transform.position, PathToFollow.WayPoints[_position].transform.position, Speed*Time.deltaTime);
			//if (CanMove(nextPosition-transform.position)) 
			transform.position = nextPosition; 
			_direction = (PathToFollow.WayPoints[_position].transform.position - transform.position).normalized;
			ChangeFacing(_direction);
			_animator.SetBool("Walking", true);
			
			if (transform.position == PathToFollow.WayPoints[_position].transform.position)
			{
				_animator.SetInteger("Direction", 2);
				if (PathToFollow.WayPoints[_position].Type == PathNodeType.Dialogue)
				{
					if (!_dialogueFinished)
					{
						_isWaiting = true;
						_animator.SetBool("Walking", false);
						_newDialogue = PathToFollow.WayPoints[_position].Text;
					}
					else
					{
						_dialogueFinished = false;
						_position++;
					}
					
				}
				else if (PathToFollow.WayPoints[_position].Type == PathNodeType.Waiting)
				{
					_isWaiting = true;
					_animator.SetBool("Walking", false);
					_newDialogue = PathToFollow.WayPoints[_position].Text;
					StartCoroutine(WaitForTime(PathToFollow.WayPoints[_position].TimeToWait));
				}
				
				else
				{
					_position++;	
				}
				

				if (_position == PathToFollow.WayPoints.Count)
				{
					if(PathToFollow.Type!=PathType.Loop)
					{
						if (_newDialogue == null)
						{
							EndOfPath();
							
						}
						else
						{
							hasFinished = true;
						}
						
					}
					else
					{
						_position = 0;
						
					}
				}

				
			
			}
			
			
			
		}

		if (_inDialogue && !TextBoxManager.Instance.IsActive)
		{
			_isWaiting = false;
			_inDialogue = false;
			_dialogueFinished = true;
			Debug.Log("Exit Dialogue");
			_newDialogue = null;
			ChangeFacing(_direction);
			_animator.SetBool("Walking", _walk);
		}
		if (hasFinished && _newDialogue==null)
		{
			EndOfPath();
		}

	}

	private IEnumerator WaitForTime(float Time)
	{
		
		yield return new WaitForSeconds(Time);
		_isWaiting = false;
		_position++;
		
		if (_position == PathToFollow.WayPoints.Count)
		{
			if(PathToFollow.Type!=PathType.Loop)
			{
				if (_newDialogue == null)
				{
					EndOfPath();
							
				}
				else
				{
					hasFinished = true;
				}
						
			}
			else
			{
				_position = 0;
				Debug.Log(_position);
			}
		}
	}

	private void EndOfPath()
	{
		Destroy(PathToFollow.gameObject);
		_animator.SetBool("Walking", false);
		if (GetComponent<NPCInteractable>() != null)
		{
			GetComponent<NPCInteractable>().enabled = true;
		}

		
		Destroy(this);
	}

	public override void Interact()
	{
		if (_isWaiting && _newDialogue != null)
		{
			_inDialogue = true;
			_dialogueFinished = false;
			Debug.Log("Start Dialogue");
			ChangeFacing(Player.Instance.transform.position - transform.position);
			_animator.SetBool("Walking", false);
			_newDialogue.StartDialogue();
		}
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
	
	
	

	public override bool IsInteractable()
	{
		return _isWaiting;
	}
}

