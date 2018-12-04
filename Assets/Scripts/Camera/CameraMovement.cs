using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {
    

    public bool hasDeadZone;
    public float marginX;
    public float marginY;
    [Range(0,1)]
    public float smoothTime = 0.3F;

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
                transform.position = Vector3.Slerp(transform.position, Player.Instance.transform.position + offset,  smoothTime * Time.deltaTime);
        
       
        
    }

    public void TeleportCamera()
    {
        transform.position =Player.Instance.transform.position + offset;
        
    }

   
}
