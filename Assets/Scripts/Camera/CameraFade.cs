using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraFade: MonoBehaviour
{
    public static CameraFade Instance { get; private set; }
    public Image Img;

    private void Awake()
    {
        if (Instance == null)//TODO Check if this needs to be a Singleton
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public IEnumerator FadeTo(float transitionTime, float alpha)
    {
        gameObject.GetComponent<Canvas>().enabled = true;
            
        Color tmp = Img.color;

        for (float t = 0; t < 1.0f; t += Time.deltaTime / transitionTime)
        {
            tmp.a = Mathf.Lerp(tmp.a, alpha, t);
            Img.color = tmp;

            yield return null;
        }

        if (alpha == 0f)
            gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void InstantChange(float alpha)
    {
        gameObject.GetComponent<Canvas>().enabled = true;

        Color tmp = Img.color;
        tmp.a = alpha;
        Img.color = tmp;
        
        if (alpha == 0f)
            gameObject.GetComponent<Canvas>().enabled = false;
    }

    //TODO Look if it's possible to use delegate to create a function that fade in and out but also perform a custom function
}
