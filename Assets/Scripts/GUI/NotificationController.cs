using System.Collections;
using System.Collections.Generic;
using System.Text;
using Quests;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
	[Header("Notification")]
	public GameObject NotificationWindow;
	public Text Textbox;
	public Image Img;

	private bool _active;
	
	/// <summary>
	/// Sets the notification window in order to display the object and a message based on the action
	/// </summary>
	/// <param name="item">Item retrieved or removed</param>
	/// <param name="op">Operation performed: true to say that an object has been retrieved, false to say that an object has been removed</param>
	public void ShowItemNotification(Item item, bool op)
	{
		Img.enabled = true;
		Debug.Log(Img.rectTransform.anchorMax);
		Textbox.rectTransform.anchorMin = new Vector2(Img.rectTransform.anchorMax.x, 0);
		
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

		Img.sprite = item.InventoryArtwork;
		
		ShowNotification(stringBuilder);
	}

	/// <summary>
	/// Sets the notification window in order to display a message based on the state of the quest
	/// </summary>
	/// <param name="quest">Quest activated or completed</param>
	/// <param name="op">Operation performed: true to say that a quest has been activated, false to say that a quest has been completed</param>
	public void ShowQuestNotification(Quest quest, bool op)
	{
		Img.enabled = false;
		Textbox.rectTransform.anchorMin = new Vector2(0,0);
		
		StringBuilder stringBuilder = new StringBuilder();

		stringBuilder.Append("Quest [");
		stringBuilder.Append(quest.QuestID);
		
		if (op)
		{
			stringBuilder.Append("] has been taken");
		}
		else
		{
			stringBuilder.Append("] has been completed");
		}
		
		ShowNotification(stringBuilder);
	}
	
	/// <summary>
	/// Displays the message on the notification window
	/// </summary>
	/// <param name="stringBuilder">Message to show</param>
	private void ShowNotification(StringBuilder stringBuilder)
	{
		Debug.Log(stringBuilder.ToString());
		_active = true;
		NotificationWindow.SetActive(true);
		Textbox.text = stringBuilder.ToString();
	}

	/// <summary>
	/// Hides the notification window
	/// </summary>
	public void HideNotification()
	{
		NotificationWindow.SetActive(false);
		_active = false;
	}
	
	/// <summary>
	/// Checks whether the notification window is active or not
	/// </summary>
	/// <returns>True if the notification window is active, false otherwise</returns>
	public bool IsNotificationActive()
	{
		return _active;
	}
}
