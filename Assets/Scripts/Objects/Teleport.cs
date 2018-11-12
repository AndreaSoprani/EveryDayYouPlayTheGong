using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport Destination;
    public Vector3 Offset;
    
    
    private bool _teleportActive = true;
    private bool _needDeactivation;

    private void Start()
    {
        _needDeactivation = !(Offset.sqrMagnitude > 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_teleportActive)
            {  
                if(_needDeactivation)
                    Destination.StopTeleport();
              
                other.gameObject.transform.position = Destination.transform.position + Offset;
                GameObject.FindGameObjectWithTag("MainCamera").SendMessage("TeleportCamera");
            }
        }
    }

    private void StopTeleport()
    {
        _teleportActive = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _teleportActive = true;
    }
}
