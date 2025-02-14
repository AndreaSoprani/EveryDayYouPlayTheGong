﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class DayProgressionManager : MonoBehaviour {

	public static DayProgressionManager Instance { get; private set; }
    public DayNightManager DayNightManager;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public Settings Settings;
	
	public int Day = 0;
	public int DayProgress = 0;

	private void Start()
	{
		GameObject.FindGameObjectWithTag("GUIController").SendMessage("UpdateDayProgression");
	}

	/// <summary>
	/// To call upon quest completion, it makes the day progress but only if the day is not over.
	/// </summary>
	public void Progress()
	{
		if (DayProgress < Settings.QuestsPerDay)
		{
			DayProgress++;
            DayNightManager.ChangeTimeSlot();
			Debug.Log("progression: "+DayProgress);
			GameObject.FindGameObjectWithTag("GUIController").SendMessage("UpdateDayProgression");
		}
	}

	/// <summary>
	/// Call to pass to the next day, it resets the DayProgress variable.
	/// It works only if the day is over.
	/// </summary>
	public void NextDay()
	{
		if (IsDayOver())
		{
			DayProgress = 0;
			Day++;
			QuestManager.Instance.TodayQuests.Clear();
			QuestManager.Instance.PopQuestsOnHold();
            DayNightManager.ChangeTimeSlot();
            GameObject.FindGameObjectWithTag("GUIController").SendMessage("UpdateDayProgression");
		}
	}

	/// <summary>
	/// Used to check if the day is over and the gong must be played.
	/// </summary>
	/// <returns>True if the Day is over, false otherwise.</returns>
	public bool IsDayOver()
	{
		return DayProgress == Settings.QuestsPerDay;
	}

}
