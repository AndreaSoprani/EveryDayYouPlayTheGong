using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class CameraMovement : MonoBehaviour {
    

    public bool hasDeadZone;
    public float marginX;
    public float marginY;
    
    
    public float smoothTime = 1;

    private Vector3 offset;
    private Vector2 _dynamicOffset;
    private bool _isFollowingSomeone;
    
    

    void Start () {
        
        offset = transform.position - Player.Instance.transform.position;
       
    }


    void LateUpdate()
    {
       
            _dynamicOffset = transform.position - Player.Instance.transform.position;
            if (!hasDeadZone || Mathf.Abs(_dynamicOffset.x) > marginX || Mathf.Abs(_dynamicOffset.y) > marginY)
                transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position + offset,  smoothTime * Player.Instance.Velocity * Time.deltaTime);
        
       
        
    }

    public void TeleportCamera()
    {
        transform.position =Player.Instance.transform.position + offset;
        
    }

   
}
