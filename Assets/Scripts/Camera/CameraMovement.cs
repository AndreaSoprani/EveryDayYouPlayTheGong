using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {
    public GameObject player;

    public bool hasDeadZone;
    public float marginX;
    public float marginY;
    [Range(0,1)]
    public float smoothTime = 0.3F;

    private Vector3 offset;
    private Vector2 _dynamicOffset;
    
    //This variable has been introduced to reduce bounce back after being teleported
    private bool _hasTeleported;
    
   
   

    void Start () {
        
        offset = transform.position - player.transform.position;
	}


    void LateUpdate()
    {
        if (!hasDeadZone || Mathf.Abs(_dynamicOffset.x) > marginX || Mathf.Abs(_dynamicOffset.y) > marginY)
            transform.position = Vector3.Slerp(transform.position, player.transform.position + offset, (_hasTeleported ? 1 : smoothTime) * Time.deltaTime);
        if (_hasTeleported)
            _hasTeleported = false;
    }

    public void TeleportCamera()
    {
        transform.position = player.transform.position +offset;
        _hasTeleported = true;
    }
}
