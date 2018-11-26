using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	
	// Stores all the keycodes for the player's actions.
	private IDictionary<string, KeyCode> _actionKeyCodes;
	
	private Transform _tr;

	// Direction in which the player is facing.
	private Vector3 _facing;

	// List of all the items possessed by the player.
	private Collection<Item> _items;

	private bool _inDialogue;

	private Animator _animator;
	
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
		
		_facing = Vector3.up;
		
		_items = new Collection<Item>();
		//TODO: initialize as checkpoint.

		_inDialogue = false;

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (_inDialogue) return;
		
		Move();

		IInteractiveObject interactiveObject = HasInteraction();
		
		if (interactiveObject != null && Input.GetKeyDown(_actionKeyCodes["Interact"]))
		{
			Debug.Log("Interact");
			Interact(interactiveObject);
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
		
		if (Input.GetKey(_actionKeyCodes["Run"]) && !Input.GetKey(_actionKeyCodes["Crawl"]))
			movementVelocity *= RunIncrement;
		if (Input.GetKey(_actionKeyCodes["Crawl"]))
			movementVelocity *= CrawlDecrement;
		
		float deltaHorizontal = Input.GetAxis("Horizontal") * movementVelocity * Time.deltaTime;
		float deltaVertical = Input.GetAxis("Vertical") * movementVelocity * Time.deltaTime;

		Vector3 movement = new Vector3(deltaHorizontal, deltaVertical, 0);
		
		if (movement.Equals(Vector3.zero))
		{
			_animator.SetBool("Walking", false);
		}
		else
		{
			if(!_animator.GetBool("Walking"))
				_animator.SetBool("Walking", true);
			
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
			
			IPlayableObject pObject = hit.collider.gameObject.GetComponent<IPlayableObject>();
			if (pObject != null && pObject.IsPlayable())
			{
				pObject.Play();
				EventManager.TriggerEvent("Play" + pObject);
				break;
			}
		}
		
	}

	/// <summary>
	/// Calls the Interaction on the Interactive Object
	/// </summary>
	/// <param name="obj">GameObject to interact with.</param>
	void Interact(IInteractiveObject obj)
	{
		obj.Interact(this);
		EventManager.TriggerEvent("Interact" + obj);
		Debug.Log(_items.Count);
		
	}

	/// <summary>
	/// Allows to detect an interactive object in the facing direction (within the InteractionDetectionRadius range).
	/// </summary>
	/// <returns>the object detected or null if nothing was detected.</returns>
	IInteractiveObject HasInteraction()
	{
		Collection<Vector3> positions = RayCastPositions.Vector3ToRayCastPosition(_facing);

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(positions[i], _facing, InteractionDetectionRadius, ObstacleLayer);
			if (hit.collider == null) continue;
			
			IInteractiveObject iObject = hit.collider.gameObject.GetComponent<IInteractiveObject>();
			if (iObject != null && iObject.IsInteractable())
			{
				//EventManager.TriggerEvent("InteractionGUIElement");
				return iObject;
			}
		}

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
		
		if (removed) EventManager.TriggerEvent("RemoveItem" + item.Id);
		
		// TODO: display object removed text.
		
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
	}

	/**
	 * Used to exit dialogue mode.
	 * Control on player is re-enabled.
	 */
	public void ExitDialogue()
	{
		_inDialogue = false;
	}

	/**
	 * Used to check if the player is in dialogue.
	 */
	public bool IsInDialogue()
	{
		return _inDialogue;
	}
	
}
