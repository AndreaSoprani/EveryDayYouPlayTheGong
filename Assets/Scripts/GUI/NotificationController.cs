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
	public Text ItemTextbox;
	public Text QuestTextbox;
	public Text ExitText;
	public Image Img;


	private bool _active;
	private bool _canBeClosed;
	
	/// <summary>
	/// Sets the notification window in order to display the object and a message based on the action
	/// </summary>
	/// <param name="item">Item retrieved or removed</param>
	/// <param name="op">Operation performed: true to say that an object has been retrieved, false to say that an object has been removed</param>
	public void ShowItemNotification(Item item, bool op)
	{
		Img.enabled = true;
		
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
		
		QuestTextbox.enabled = false;
		
		ShowNotification(stringBuilder, ItemTextbox);
	}

	/// <summary>
	/// Sets the notification window in order to display a message based on the state of the quest
	/// </summary>
	/// <param name="quest">Quest activated or completed</param>
	/// <param name="op">Operation performed: true to say that a quest has been activated, false to say that a quest has been completed</param>
	public void ShowQuestNotification(Quest quest, bool op)
	{
		Img.enabled = false;
		
		StringBuilder stringBuilder = new StringBuilder();
		
		if (op)
		{
			stringBuilder.Append("New quest: ");
		}
		else
		{
			stringBuilder.Append("Quest completed: ");
		}

		stringBuilder.Append(quest.Name);

		ItemTextbox.enabled = false;
		
		ShowNotification(stringBuilder, QuestTextbox);
	}
	
	/// <summary>
	/// Displays the message on the notification window
	/// </summary>
	/// <param name="stringBuilder">Message to show</param>
	/// <param name="textBox">Textbox that is going to be displayed</param>
	private void ShowNotification(StringBuilder stringBuilder, Text textBox)
	{
		Debug.Log(stringBuilder.ToString());
		_canBeClosed = false;
		_active = true;
		textBox.text = stringBuilder.ToString();
		textBox.enabled = true;
		
		NotificationWindow.SetActive(true);
		//TODO Add music here
		StartCoroutine(DelayClose());

	}

	private IEnumerator DelayClose()
	{
		yield return new WaitForSecondsRealtime(1f);	//TODO Change time based on tune
		ExitText.enabled = true;
		_canBeClosed = true;
	}

	/// <summary>
	/// Hides the notification window
	/// </summary>
	public void HideNotification()
	{
		NotificationWindow.SetActive(false);
		_active = false;
		ExitText.enabled = false;
	}
	
	/// <summary>
	/// Checks whether the notification window is active or not
	/// </summary>
	/// <returns>True if the notification window is active, false otherwise</returns>
	public bool IsNotificationActive()
	{
		return _active;
	}
	
	/// <summary>
	/// Checks whether the notification window can be closed or not
	/// </summary>
	/// <returns>True if the notification window can be closed, false otherwise</returns>
	public bool CanBeClosed()
	{
		return _canBeClosed;
	}
}
