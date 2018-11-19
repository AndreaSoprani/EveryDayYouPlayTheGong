using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneInfo: MonoBehaviour
{
    [Header("This scene")]
    public string ThisScene;
    
    [Header("Adjacent scenes")]
    public List<string> SceneNames;

    private void Start()
    {
        Debug.Log("Started this scene: " + ThisScene);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.Instance.LoadNewScenes(ThisScene, SceneNames);
        }
    }
}
