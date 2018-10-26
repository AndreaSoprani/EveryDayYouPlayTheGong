using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTR : MonoBehaviour
{

	public float Velocity = 0.2f;

	private Transform _tr;
	
	// Use this for initialization
	void Start ()
	{
		_tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		_tr.position += new Vector3(horizontal*Velocity, vertical*Velocity, 0);
	}
}
