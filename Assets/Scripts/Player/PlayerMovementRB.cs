using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementRB : MonoBehaviour {
    
    private Rigidbody2D rb2d;
    public float velocity = 1;
    public float maxVelocity = 4;
    
	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	void FixedUpdate () {
	
	    float horizontal = Input.GetAxis("Horizontal");
	    float vertical = Input.GetAxis("Vertical");
        if (rb2d.velocity.y >= maxVelocity && vertical > 0f)
            vertical = 0f;
        if (rb2d.velocity.x >= maxVelocity && horizontal > 0f)
            horizontal = 0f;
        rb2d.AddForce(new Vector2(horizontal,vertical)*velocity);
	}

    
}
