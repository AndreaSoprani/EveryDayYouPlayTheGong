using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("LayerSwitch"))
            other.GetComponentInParent<SpriteRenderer>().sortingLayerName = "CharacterBehind";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("LayerSwitch"))
            other.GetComponentInParent<SpriteRenderer>().sortingLayerName = "CharacterFront";
    }
}
