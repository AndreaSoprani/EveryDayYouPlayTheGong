using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTR : MonoBehaviour
{

	public float Velocity = 2f;
	public float RunIncrement = 1.2f;
	public float CrawlDecrement = 0.7f;
	
	
	private Transform _tr;
	
	// Use this for initialization
	void Start ()
	{
		_tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float movementVelocity=Velocity;
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
			movementVelocity *= RunIncrement;
		if (Input.GetKey(KeyCode.LeftShift))
			movementVelocity *= CrawlDecrement;
		
		//Debug.Log("velocity " +	 movementVelocity.ToString());
		_tr.position += new Vector3(horizontal * movementVelocity * Time.deltaTime, vertical * movementVelocity * Time.deltaTime,
				0);
	}
}
