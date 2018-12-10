using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIButton : MonoBehaviour
{

	public Sprite OnSelectedImage;
	public Sprite OnUnSelectedImage;

	
	public void UpdateImage(bool value)
	{
		gameObject.GetComponent<Image>().sprite = value ? OnSelectedImage : OnUnSelectedImage;
	}

	
}
