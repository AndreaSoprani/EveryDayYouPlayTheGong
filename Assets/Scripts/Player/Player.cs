using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Player : MonoBehaviour
{

	[Header("Movement")]
	public float Velocity = 2f;
	public float RunIncrement = 1.2f;
	public float CrawlDecrement = 0.7f;

	[Header("Collisions")] 
	public RayCastPositions RayCastPositions;
	public LayerMask ObstacleLayer;
	public LayerMask InteractiveLayer;
	public LayerMask PlayableLayer;

	[Header("Interaction")] public float InteractionDetectionRadius = 1f;
	
	// Stores all the keycodes for the player's actions.
	private IDictionary<string, KeyCode> _actionKeyCodes;
	
	private Transform _tr;

	// Direction in which the player is facing.
	private Vector3 facing;
	
	// Use this for initialization
	void Start ()
	{
		_tr = GetComponent<Transform>();

		//TODO: load keys from file + add edit
		_actionKeyCodes = new Dictionary<string, KeyCode>()
		{
			{"Run", KeyCode.LeftControl},
			{"Crawl", KeyCode.LeftShift},
			{"Play", KeyCode.Space},
			{"Interact", KeyCode.X}
		};
		
		facing = Vector3.up;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();

		GameObject interactiveObject = HasInteraction();
		
		if (interactiveObject != null && Input.GetKeyDown(_actionKeyCodes["Interact"]))
		{
			Interact(interactiveObject);
		} else if (Input.GetKeyDown(_actionKeyCodes["Play"]))
		{
			Play();
		}
		
	}

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
		
		UpdateFacing(new Vector3(deltaHorizontal, deltaVertical, 0));
		
		if (CanMoveHorizontally(deltaHorizontal))
		{
			_tr.position += new Vector3(deltaHorizontal, 0, 0);
		} 
		if (CanMoveVertically(deltaVertical))
		{
			_tr.position += new Vector3(0, deltaVertical, 0);
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
			RaycastHit2D hit = Physics2D.Raycast(positions[i], _tr.right * Math.Sign(delta), delta, ObstacleLayer);
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
			RaycastHit2D hit = Physics2D.Raycast(positions[i], _tr.up * Math.Sign(delta), delta, ObstacleLayer);
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
		if (movement.Equals(Vector3.zero)) return;
		float angle = Vector3.SignedAngle(Vector3.right, movement, Vector3.forward);
		Vector3 newFacing;
		
		if(angle > -45f && angle < 45f) newFacing = Vector3.right;
		else if (angle >= 45f && angle <= 135f) newFacing = Vector3.up;
		else if (angle > 135f || angle < -135f) newFacing = Vector3.left;
		else newFacing = Vector3.down;

		if (!facing.Equals(newFacing))
		{
			facing = newFacing;
			Debug.Log(facing.ToString());
			//TODO: update animation.
		}
	}

	void Play()
	{
		//TODO: implement
	}

	/// <summary>
	/// Triggers an event called "Interact" + name of the GameObject.
	/// </summary>
	/// <param name="obj">GameObject to interact with.</param>
	void Interact(GameObject obj)
	{
		// Triggers the event to interact with the object.
		EventManager.TriggerEvent("Interact" + obj.ToString());
	}

	/// <summary>
	/// Allows to detect an interactive object in the facing direction (within the InteractionDetectionRadius range).
	/// </summary>
	/// <returns>the object detected or null if nothing was detected.</returns>
	GameObject HasInteraction()
	{
		Collection<Vector3> positions = RayCastPositions.Vector3ToRayCastPosition(facing);

		for (int i = 0; i < positions.Count; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(positions[i], facing, InteractionDetectionRadius, InteractiveLayer);
			if (hit.collider != null)
			{
				EventManager.TriggerEvent("InteractionGUIElement");
				return hit.collider.transform.parent.gameObject;
			}
		}

		return null;

	}
	
}
