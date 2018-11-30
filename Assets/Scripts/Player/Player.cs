using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Objects;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Player : MonoBehaviour
{

	private static Player _instance;
	public static Player Instance { get ; private set; }
	
	[Header("Movement")]
	public float Velocity = 2f;
	public float RunIncrement = 1.2f;
	public float CrawlDecrement = 0.7f;

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
	
	// Stores all the keycodes for the player's actions.
	private IDictionary<string, KeyCode> _actionKeyCodes;
	
	private Transform _tr;

	// Direction in which the player is facing.
	private Vector3 _facing;

	// List of all the items possessed by the player.
	private Collection<Item> _items;

	private bool _inDialogue;

	private bool _frontInteractable;
	
	private Animator _animator;
	private Canvas _canvas;
	
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

		//TODO: load keys from file + add edit
		_actionKeyCodes = new Dictionary<string, KeyCode>
		{
			{"Run", KeyCode.LeftControl},
			{"Crawl", KeyCode.LeftShift},
			{"Play", KeyCode.Space},
			{"Interact", KeyCode.X}
		};

		_animator = GetComponent<Animator>();
		_canvas = GetComponentInChildren<Canvas>();
		
		_facing = Vector3.down;
		
		_items = new Collection<Item>();
		//TODO: initialize as checkpoint.

		_inDialogue = false;

		_onFrontalStairs = false;
		_onRightStairs = false;
		_onLeftStairs = false;
		
		EventManager.StartListening("EnterDialogue", EnterDialogue);

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (_inDialogue) return;
		
		Move();

		InGameObject interactable = HasInteraction();
		
		if (interactable != null && Input.GetKeyDown(_actionKeyCodes["Interact"]))
		{
			Interact(interactable);
		} else if (Input.GetKeyDown(_actionKeyCodes["Play"]))
		{
			Play();
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
		bool run = false;

		if (Input.GetKey(_actionKeyCodes["Run"]) && !Input.GetKey(_actionKeyCodes["Crawl"]))
		{
			movementVelocity *= RunIncrement;
			run = true;
		}
		if (Input.GetKey(_actionKeyCodes["Crawl"]))
			movementVelocity *= CrawlDecrement;
		
		float deltaHorizontal = Input.GetAxis("Horizontal") * movementVelocity * Time.deltaTime;
		float deltaVertical = Input.GetAxis("Vertical") * movementVelocity * Time.deltaTime;

		// Stairs checking
		if (_onFrontalStairs)
		{
			if (deltaVertical > 0) deltaVertical = deltaVertical * StairsSpeedModifier;
			else if (deltaVertical < 0) deltaVertical = deltaVertical / StairsSpeedModifier;
		}
		else if (_onRightStairs)
		{
			if (deltaHorizontal > 0) deltaVertical = deltaVertical + StairsPositionDisplacement;
			else if (deltaHorizontal < 0) deltaVertical = deltaVertical - StairsPositionDisplacement;
		} 
		else if (_onLeftStairs)
		{
			if (deltaHorizontal > 0) deltaVertical = deltaVertical - StairsPositionDisplacement;
			else if (deltaHorizontal < 0) deltaVertical = deltaVertical + StairsPositionDisplacement;
		}
		
		Vector3 movement = new Vector3(deltaHorizontal, deltaVertical, 0);
		
		if (movement.Equals(Vector3.zero))
		{
			_animator.SetBool("Walking", false);
		}
		else
		{
			_animator.SetBool("Walking", true);
			
			_animator.SetBool("Running", run);
			
			UpdateFacing(movement);

			if (CanMoveHorizontally(deltaHorizontal))
			{
				_tr.position += new Vector3(deltaHorizontal, 0, 0);
			}

			if (CanMoveVertically(deltaVertical))
			{
				_tr.position += new Vector3(0, deltaVertical, 0);
			}
		}
	}

	/// <summary>
	/// Checks if the player can move horizontally.
	/// </summary>
	/// <param name="delta">the quantity of movement intended to perform</param>
	/// <returns>true if the player can move of delta horizontally, false otherwise.</returns>
	bool CanMoveHorizontally(float delta)
	{
		Collection<Vector3> positions = new Collection<Vector3>();

		if (delta > 0) positions = RayCastPositions.Vector3ToRayCastPosition(Vector3.right); // Going right
		else if (delta < 0) positions = RayCastPositions.Vector3ToRayCastPosition(Vector3.left); // Going left

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Linecast(positions[i], positions[i] + _tr.right * delta, ObstacleLayer);
			if (hit.collider != null) return false;
		}
		
		return true;
	}

	/// <summary>
	/// Checks if the player can move vertically.
	/// </summary>
	/// <param name="delta">the quantity of movement intended to perform</param>
	/// <returns>true if the player can move of delta vertically, false otherwise.</returns>
	bool CanMoveVertically(float delta)
	{
		Collection<Vector3> positions = new Collection<Vector3>();

		if (delta > 0) positions = RayCastPositions.Vector3ToRayCastPosition(Vector3.up); // Going up
		else if (delta < 0) positions = RayCastPositions.Vector3ToRayCastPosition(Vector3.down); // Going down

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Linecast(positions[i], positions[i] + _tr.up * delta, ObstacleLayer);
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
		_animator.SetTrigger("Playing");
		
		Collection<Vector3> positions = RayCastPositions.Vector3ToRayCastPosition(_facing);

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(positions[i], _facing, PlayRadius, ObstacleLayer);
			if (hit.collider == null) continue;
			
			InGameObject pObject = hit.collider.gameObject.GetComponent<InGameObject>();
			if (pObject != null && pObject.IsPlayable())
			{
				pObject.Play();
				EventManager.TriggerEvent("Play" + pObject.ObjectID);
				break;
			}
		}
		
	}

	/// <summary>
	/// Calls the Interaction on the Interactive Object
	/// </summary>
	/// <param name="obj">InGameObject to interact with.</param>
	void Interact(InGameObject obj)
	{
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
		//GameObject.FindGameObjectWithTag("Inventory").SendMessage("UpdateItems");
		//string message = "Item acquired: " + item.Name;
		//TextBoxManager.Instance.LoadScript(message, "");
		//TextBoxManager.Instance.EnableTextBox();
		
		GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyGetItem", item);
		EventManager.TriggerEvent("AddItem" + item.Id);
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
	
	/**
	 * Used to enter in dialogue mode.
	 * All control on the player is disabled.
	 */
	public void EnterDialogue()
	{
		_inDialogue = true;
		EventManager.StopListening("EnterDialogue", EnterDialogue);
		EventManager.StartListening("ExitDialogue", ExitDialogue);
	}

	/**
	 * Used to exit dialogue mode.
	 * Control on player is re-enabled.
	 */
	public void ExitDialogue()
	{
		_inDialogue = false;
		EventManager.StopListening("ExitDialogue", ExitDialogue);
		EventManager.StartListening("EnterDialogue", EnterDialogue);
	}

	/**
	 * Used to check if the player is in dialogue.
	 */
	public bool IsInDialogue()
	{
		return _inDialogue;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
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
}
