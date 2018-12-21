using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> _eventDictionary;
	private List<string> _blockedEvents;

	private static EventManager _instance;

	public static EventManager Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = FindObjectOfType (typeof (EventManager)) as EventManager;

				if (!_instance) 
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					_instance.Init (); 
				}
			}

			return _instance;
		}
	}

	void Init ()
	{
		if (_eventDictionary == null)
		{
			_eventDictionary = new Dictionary<string, UnityEvent>();
		}

		if (_blockedEvents == null) _blockedEvents = new List<string>();
	}

	public static void StartListening (string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (Instance._eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			Instance._eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener)
	{
		if (_instance == null) return;
		UnityEvent thisEvent = null;
		if (Instance._eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		if (IsBlocked(eventName)) Instance._blockedEvents.Remove(eventName);
		
		UnityEvent thisEvent = null;
		if (Instance._eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}

	public static void BlockEvent(string eventName)
	{
		if(!Instance._blockedEvents.Contains(eventName)) Instance._blockedEvents.Add(eventName);
	}

	public static bool IsBlocked(string eventName)
	{
		return Instance._blockedEvents.Contains(eventName);
	}
	
}