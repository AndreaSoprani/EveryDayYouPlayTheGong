using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Quests;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityScript.Steps;
using Utility;

public class NotificationController : MonoBehaviour
{
	[Header("Notification")]
	public GameObject NotificationWindow;
	public TextMeshProUGUI ItemTextbox;
	public TextMeshProUGUI QuestTextbox;
	public TextMeshProUGUI ExitText;
	public Image Img;
	public Settings Settings;


	private bool _active;
	private bool _canBeClosed;
	private List<Notification> _notificationsBuffer = new List<Notification>();


	public void InsertNotification(Notification notification)
	{
		if(notification != null) _notificationsBuffer.Add(notification);
	}

	public void DisplayNotification()
	{
		if (_active || _notificationsBuffer.Count == 0) return;

		Notification notification = _notificationsBuffer[0];
		_notificationsBuffer.Remove(notification);

		Time.timeScale = 0;

		if (notification.GetType() == typeof(QuestNotification))
		{
			QuestNotification questNotification = (QuestNotification) notification;
			ShowQuestNotification(questNotification.Quest, questNotification.Op);
		}
		else if (notification.GetType() == typeof(ItemNotification))
		{
			ItemNotification itemNotification = (ItemNotification) notification;
			ShowItemNotification(itemNotification.Item, itemNotification.Op);
		}

	}
	
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
	private void ShowNotification(StringBuilder stringBuilder, TextMeshProUGUI textBox)
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
		yield return new WaitForSecondsRealtime(Settings.NotificationsDelayTime);
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

public abstract class Notification{}

public class QuestNotification : Notification
{
	public Quest Quest;
	public bool Op; // true = new, false = completed

	public QuestNotification(Quest quest, bool op)
	{
		Quest = quest;
		Op = op;
	}
}

public class ItemNotification : Notification
{
	public Item Item;
	public bool Op; // true = retrieved, false = removed

	public ItemNotification(Item item, bool op)
	{
		Item = item;
		Op = op;
	}
}