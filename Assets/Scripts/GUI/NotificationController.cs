using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
	[Header("Item notification")]
	public GameObject ItemNotification;
	public Text ItemText;
	public Image Img;
	
	
	public void ShowNotification(Item item, bool op)
	{
		ItemNotification.SetActive(true);
		
		StringBuilder stringBuilder = new StringBuilder();

		stringBuilder.Append(item.Name);
		
		if (op)
		{
			stringBuilder.Append(" has been retrieved");
		}
		else
		{
			stringBuilder.Append(" has been removed");
		}

		ItemText.text = stringBuilder.ToString();

		Img.sprite = item.InventoryArtwork;
		
	}

	public void HideNotification()
	{
		ItemNotification.SetActive(false);
	}
	
	public bool IsNotificationActive()
	{
		return ItemText.IsActive();
	}
}
