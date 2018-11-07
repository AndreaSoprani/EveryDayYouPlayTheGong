using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[Header("Movement")]
	public float Velocity = 2f;
	public float RunIncrement = 1.2f;
	public float CrawlDecrement = 0.7f;

	[Header("RayCast Directions")]
	public Transform RayCastUp;
	public Transform RayCastDown;
	public Transform RayCastRight;
	public Transform RayCastLeft;

	[Header("Collisions")]
	public LayerMask ObstacleLayer;
	
	// Stores all the keycodes for the player's actions.
	private IDictionary<string, KeyCode> _actionKeyCodes;
	
	private Transform _tr;
	
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

	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();

		if (Input.GetKeyDown(_actionKeyCodes["Play"]))
		{
			Play();
		} else if (Input.GetKeyDown(_actionKeyCodes["Interact"]))
		{
			Interact();
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
		Vector3 position = Vector3.zero;

		if (delta > 0) position = RayCastRight.position; // Going right
		else if (delta < 0) position = RayCastLeft.position; // Going left

		RaycastHit2D hit = Physics2D.Raycast(position, _tr.right * Math.Sign(delta), delta, ObstacleLayer);

		if (hit.collider != null) return false;
		else return true;
	}

	/// <summary>
	/// Checks if the player can move vertically.
	/// </summary>
	/// <param name="delta">the quantity of movement intended to perform</param>
	/// <returns>true if the player can move of delta vertically, false otherwise.</returns>
	bool CanMoveVertically(float delta)
	{
		Vector3 position = Vector3.zero;

		if (delta > 0) position = RayCastUp.position; // Going up
		else if (delta < 0) position = RayCastDown.position; // Going down

		RaycastHit2D hit = Physics2D.Raycast(position, _tr.up * Math.Sign(delta), delta, ObstacleLayer);

		if (hit.collider != null) return false;
		else return true;
	}

	void Play()
	{
		//TODO: implement
	}

	void Interact()
	{
		//TODO: implement
	}
	
}
