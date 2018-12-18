using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class CameraMovement : MonoBehaviour {
    

    public bool hasDeadZone;
    public float marginX;
    public float marginY;
    
    
    public float smoothTime;

    private Vector3 offset;
    private Vector2 _dynamicOffset;
    private bool _isFollowingSomeone;
    
    

    void Start () {
        
        offset = transform.position - Player.Instance.transform.position;
       
    }


    void LateUpdate()
    {
       
        _dynamicOffset = transform.position - Player.Instance.transform.position;

        float factor = (Player.Instance.IsRunning() ? smoothTime * Player.Instance.RunIncrement : smoothTime) * Time.deltaTime;
        
        if (!hasDeadZone || Mathf.Abs(_dynamicOffset.x) > marginX || Mathf.Abs(_dynamicOffset.y) > marginY)
            transform.position = Vector3.Slerp(transform.position, Player.Instance.transform.position + offset, factor);
        
       
        
    }

    public void TeleportCamera()
    {
        transform.position =Player.Instance.transform.position + offset;
        
    }

   
}
