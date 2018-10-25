using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {
    public GameObject player;

    public bool hasDeadZone=false;
    public float marginX=0f;
    public float marginY=0f;

    public float smoothTime = 0.3F;

    private Vector3 offset;
   
   

    void Start () {
        
        offset = transform.position - player.transform.position;
	}


    void LateUpdate()
    {

        if (!hasDeadZone || (Mathf.Abs(transform.position.x - player.transform.position.x) > marginX || Mathf.Abs(transform.position.y - player.transform.position.y) > marginY))
            transform.position = Vector3.Slerp(transform.position, player.transform.position + offset, smoothTime * Time.deltaTime);
    }
}
