﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Utility;

public class Player : MonoBehaviour
{

	private static Player _instance;
	public static Player Instance { get ; private set; }
	
	public Settings Settings;
	
	[Header("Movement")]
	public float Velocity = 2f;
	public float RunIncrement = 1.2f;
	

	[Header("Collisions")] 
	public RayCastPositions RayCastPositions;
	public LayerMask ObstacleLayer;

	[Header("Interact & Play")] 
	public float InteractionDetectionRadius = 1f;
	public float PlayRadius = 1f;

	[Header("Stairs")] 
	[Range(0.1f, 1f)] public float StairsSpeedModifier = 0.5f; // Modifier for the speed when on frontal stairs.
	[Range(0f, 0.1f)] public float StairsPositionDisplacement = 0.01f; // Modifier for position upgrade when on a side stair
	private bool _onFrontalStairs;
	private bool _onRightStairs;
	private bool _onLeftStairs;
	
	private Transform _tr;

	// Direction in which the player is facing.
	private Vector3 _facing;
	
	private Dictionary<Vector3, KeyCode> _directionsKeyCodes;
	private List<Vector3> _directionsPile;

	// List of all the items possessed by the player.
	private Collection<Item> _items;

	private bool _inDialogue;
	private bool _blocked;
	private bool _frontInteractable;
	private bool _running;

	private List<InGameObject> _playableObjectsFound;
	
	private Animator _animator;
	private Canvas _canvas;
	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _playingArea;
	private Collider2D _playerCollider;

	private Vector2 _offsetValue;
	
	private bool _goBack = false;
	private Vector3 _goBackPosition;
	
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		_tr = GetComponent<Transform>();

		_animator = GetComponent<Animator>();
		_canvas = GetComponentInChildren<Canvas>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_playingArea = transform.Find("PlayingArea").GetComponent<BoxCollider2D>();
		_playerCollider = GetComponent<Collider2D>();
		
		_offsetValue = new Vector2(1.2f, 0);
		
		_facing = Vector3.down;
		_directionsKeyCodes = Settings.GetDirectionsKeyCodes();
		_directionsPile = new List<Vector3>();
		
		_items = new Collection<Item>();

		_inDialogue = false;
		_running = false;

		_onFrontalStairs = false;
		_onRightStairs = false;
		_onLeftStairs = false;
		
		_playableObjectsFound = new List<InGameObject>();
		
		EventManager.StartListening("EnterDialogue", EnterDialogue);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_inDialogue || _blocked) return;

		if (!_goBack)
		{
			Move();
		

			InGameObject interactable = HasInteraction();
			
			if (interactable != null && Input.GetKeyDown(Settings.Interact))
			{
				Interact(interactable);
			} else if (Input.GetKeyDown(Settings.Play) && !_animator.GetBool("Playing"))
			{
				Play();
			}
		}
		else
		{
			if (_tr.position == _goBackPosition) _goBack = false;
			else _tr.position =Vector3.MoveTowards(_tr.position,_goBackPosition,Velocity * Time.deltaTime); 
		}
	}

	/*
	 * MOVEMENT
	 */
	
	/// <summary>
	/// Calculates the velocity of the player and updates the position.
	/// Must be called once per frame.
	/// </summary>
	void Move()
	{
		float movementVelocity = Velocity;

		if (Input.GetKey(Settings.Run))
		{
			movementVelocity *= RunIncrement;
			_running = true;
		}
		else
		{
			_running = false;
		}
		

		foreach (Vector3 dir in _directionsKeyCodes.Keys)
		{
			KeyCode keyCode = _directionsKeyCodes[dir];
			if (Input.GetKeyDown(keyCode)) _directionsPile.Add(dir);
			if (Input.GetKeyUp(keyCode)) _directionsPile.RemoveAll(d => d == dir);
		}

		Vector3 delta = Vector3.zero;
		if (_directionsPile.Count > 0) delta = _directionsPile[_directionsPile.Count-1];

		if (delta.x > 0 && _playingArea.offset.x < 0f)
		{
			_playingArea.offset = _playingArea.offset + _offsetValue;
			_animator.SetInteger("PlayingDirection", 0);
		}
		else if (delta.x < 0 && _playingArea.offset.x > 0f)
		{
			_playingArea.offset = _playingArea.offset - _offsetValue;
			_animator.SetInteger("PlayingDirection", 1);
		}

		delta *= movementVelocity * Time.deltaTime;
		
		// Stairs checking
		if (_onFrontalStairs)
		{
			if (delta.y > 0) delta *= StairsSpeedModifier;
			else if (delta.y < 0) delta /= StairsSpeedModifier;
		}
		else if (_onRightStairs)
		{
			if (delta.x > 0) delta.y += StairsPositionDisplacement;
			else if (delta.x < 0) delta.y -= StairsPositionDisplacement;
		} else if (_onLeftStairs)
		{
			if (delta.x > 0) delta.y -= StairsPositionDisplacement;
			else if (delta.x < 0) delta.y += StairsPositionDisplacement;
		}
		
		// Animation checking and movement performing
		if (delta.Equals(Vector3.zero) || _animator.GetBool("Playing"))
		{
			_animator.SetBool("Walking", false);
		}
		else
		{
			_animator.SetBool("Walking", true);
			
			_animator.SetBool("Running", _running);
			
			UpdateFacing(delta);

			if (CanMove(delta)) _tr.position += delta;
		}
	}

	/// <summary>
	/// Checks if the player can move given a movement vector.
	/// </summary>
	/// <param name="delta">the movement to perform.</param>
	/// <returns>true if the player can move, false otherwise.</returns>
	bool CanMove(Vector3 delta)
	{
		Collection<Vector3> positions = RayCastPositions.Vector3ToRayCastPosition(delta);
		foreach (Vector3 pos in positions)
		{
			RaycastHit2D hit = Physics2D.Linecast(pos, pos + delta, ObstacleLayer);
			if (hit.collider != null) return false;
		}

		return true;
	}

	/// <summary>
	/// Used to check if the facing Vector must be updated and in case updates it.
	/// </summary>
	/// <param name="movement">The Vector3 for the next movement</param>
	void UpdateFacing(Vector3 movement)
	{
		float angle = Vector3.SignedAngle(Vector3.right, movement, Vector3.forward);
		Vector3 newFacing;
		int anim;
		
		if (angle > -45f && angle < 45f)
		{
			newFacing = Vector3.right;
			anim = 1;
		}
		else if (angle >= 45f && angle <= 135f)
		{
			newFacing = Vector3.up;
			anim = 0;
		}
		else if (angle > 135f || angle < -135f)
		{
			newFacing = Vector3.left;
			anim = 3;
		}
		else
		{
			newFacing = Vector3.down;
			anim = 2;
		}

		if (!_facing.Equals(newFacing))
		{
			_facing = newFacing;
			
			_animator.SetInteger("Direction", anim);
		}
	}

	/*
	 * PLAY AND INTERACT
	 */
	
	/// <summary>
	/// Detects if there is an object to be played and plays it.
	/// </summary>
	void Play()
	{
		StartCoroutine(PlayAnimation());
		
		//TODO: change with collider
		foreach (InGameObject obj in _playableObjectsFound)
		{
			if (obj != null && obj.IsPlayable())
			{
				obj.Play();
				EventManager.TriggerEvent("Play" + obj.ObjectID);
				break;
			}
		}
		
	}

	private IEnumerator PlayAnimation()
	{
		BlockMovement(true);
		_animator.SetBool("Playing", true);
		
		while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayRight") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayLeft"))
			yield return null;
		
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
		
		_animator.SetBool("Playing", false);
		BlockMovement(false);
	}

	/// <summary>
	/// Calls the Interaction on the Interactive Object
	/// </summary>
	/// <param name="obj">InGameObject to interact with.</param>
	void Interact(InGameObject obj)
	{
		if (FindObjectOfType<NotificationController>().IsNotificationActive()) return;
		obj.Interact();
		EventManager.TriggerEvent("Interact" + obj.ObjectID);
		
	}

	/// <summary>
	/// Allows to detect an interactive object in the facing direction (within the InteractionDetectionRadius range).
	/// </summary>
	/// <returns>the object detected or null if nothing was detected.</returns>
	InGameObject HasInteraction()
	{
		Collection<Vector3> positions = RayCastPositions.Vector3ToRayCastPosition(_facing);

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(positions[i], _facing, InteractionDetectionRadius, ObstacleLayer);
			if (hit.collider == null) continue;
			
			InGameObject iObject = hit.collider.gameObject.GetComponent<InGameObject>();
			if (iObject != null && iObject.IsInteractable())
			{
				_canvas.enabled = true;
				return iObject;
			}
		}

		_canvas.enabled = false;
		return null;

	}
	
	/*
	 * ITEMS
	 */

	/// <summary>
	/// Tells if the player is in possession of a specific item.
	/// </summary>
	/// <param name="item">the item requested</param>
	/// <returns>true if the player has the item, false otherwise</returns>
	public bool HasItem(Item item)
	{
		if (_items == null) return false;
		return _items.Contains(item);
	}
	
	/// <summary>
	/// Tells if the player is in possession of a specific item.
	/// </summary>
	/// <param name="itemId">the ID of the item requested</param>
	/// <returns>true if the player has the item, false otherwise</returns>
	public bool HasItem(string itemId)
	{
		for (int i = 0; i < _items.Count; i++)
		{
			if (_items[i].Id == itemId) return true;
		}	
		return false;
	}

	/// <summary>
	/// Adds an item to the player's possessions.
	/// </summary>
	/// <param name="item">the item to be added</param>
	public void AddItem(Item item)
	{
		_items.Add(item);
		
		GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyGetItem", item);
		EventManager.TriggerEvent("AddItem" + item.Id);
		EventManager.TriggerEvent("CheckQuestMark");
	}

	/// <summary>
	/// Removes an item from the player's possessions.
	/// </summary>
	/// <param name="item">the item to be removed</param>
	/// <returns>true if the player possessed the item, false otherwise</returns>
	public bool RemoveItem(Item item)
	{
		bool removed = _items.Remove(item);

		if (removed)
		{
			EventManager.TriggerEvent("RemoveItem" + item.Id);
			GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyRemoveItem", item);
		}
		
		return removed;
	}

	public Collection<Item> GetItem()
	{
		return _items;
	}
	
	/// <summary>
	/// Used to enter in dialogue mode.
	/// All control on the player is disabled.
	/// </summary>
	public void EnterDialogue()
	{
		_inDialogue = true;
		_animator.SetBool("Walking", false);
		BlockMovement(true);
		EventManager.StopListening("EnterDialogue", EnterDialogue);
		EventManager.StartListening("ExitDialogue", ExitDialogue);
	}

	/// <summary>
	/// Used to exit dialogue mode.
	/// Control on player is re-enabled.
	/// </summary>
	public void ExitDialogue()
	{
		BlockMovement(false);
		_inDialogue = false;
		EventManager.StopListening("ExitDialogue", ExitDialogue);
		EventManager.StartListening("EnterDialogue", EnterDialogue);
	}

	/// <summary>
	/// Adds an InGameObject to the list of playable objects in the player's stick "trajectory"
	/// </summary>
	/// <param name="playable">The InGameObject to add.</param>
	public void SetPlayableFound(InGameObject playable)
	{
		if (playable == null) return;
		_playableObjectsFound.Add(playable);
	}

	/// <summary>
	/// Removes an InGameObject from the list of playable objects in the player's stick "trajectory".
	/// </summary>
	/// <param name="playable">The InGameObject to remove.</param>
	public void RemovePlayableFound(InGameObject playable)
	{
		if (playable == null) return;
		_playableObjectsFound.Remove(playable);
	}

	/// <summary>
	/// Used to check if the player is in dialogue.
	/// </summary>
	/// <returns></returns>
	public bool IsInDialogue()
	{
		return _inDialogue;
	}

	public void ChangeLayer(string layer)
	{
		_spriteRenderer.sortingLayerName = layer;
		_canvas.sortingLayerName = layer;
	}

	public string GetLayer()
	{
		return _spriteRenderer.sortingLayerName;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.IsTouching(_playerCollider)) return;
		
		if (other.CompareTag("StairsFront"))
		{
			_onFrontalStairs = true;
		}
		else if (other.CompareTag("StairsRight"))
		{
			_onRightStairs = true;
		} 
		else if (other.CompareTag("StairsLeft"))
		{
			_onLeftStairs = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.IsTouching(_playerCollider)) return;
		
		if (other.CompareTag("StairsFront"))
		{
			_onFrontalStairs = false;
		} 
		else if (other.CompareTag("StairsRight"))
		{
			_onRightStairs = false;
		} 
		else if (other.CompareTag("StairsLeft"))
		{
			_onLeftStairs = false;
		}
	}

	/// <summary>
	/// Controls whether the player can or cannot move
	/// </summary>
	/// <param name="blockedMovement">Whether the player can move or not</param>
	public void BlockMovement(bool blockedMovement)
	{
		_blocked = blockedMovement;
		
		if (blockedMovement)
		{
			_animator.SetBool("Walking", false);
			_directionsPile.Clear();
		}
		else
		{
			foreach (Vector3 dir in _directionsKeyCodes.Keys)
			{
				KeyCode keyCode = _directionsKeyCodes[dir];
				if (Input.GetKey(keyCode))
					_directionsPile.Add(dir);
			}
		}
	}

	public bool IsBlocked()
	{
		return _blocked;
	}
	
	public bool IsRunning()
	{
		return _running;
	}

	public void RefreshCommand()
	{
		_directionsKeyCodes = Settings.GetDirectionsKeyCodes();
	}

	public void InvertFacing(float step)
	{
		_goBack = true;
		int anim;
		_animator.SetBool("Walking", true);
		if (_facing == Vector3.left)
		{
			_facing=Vector3.right;
			anim = 1;
		}
		else if (_facing == Vector3.right)
		{
			_facing=Vector3.left;
			anim = 3;
		}
		else if (_facing == Vector3.up)
		{
			_facing=Vector3.down;
			anim = 2;
		}
		else
		{
			_facing=Vector3.up;
			anim = 0;
		}
		Debug.Log(_facing+" "+anim);
		
		_animator.SetInteger("Direction", anim);
		_goBackPosition= _tr.position + _facing * step;
		
		
		
		
		
			
	}
}
