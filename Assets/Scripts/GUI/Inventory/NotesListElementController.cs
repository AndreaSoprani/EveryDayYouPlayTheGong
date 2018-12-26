using System.Collections;
using System.Collections.Generic;
using System.Text;
using Quests;
using Quests.Objectives;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotesListElementController : MonoBehaviour {

	private Quest _quest;
	
	public TextMeshProUGUI NameField;
	
	private float _top;
	private float _bottom;
	
	private Scrollbar _scrollbar;
	

	public void SetQuest(Quest quest, float top, float bottom, Scrollbar scrollbar)
	{
		_quest = quest;
		NameField.text = _quest.Name;

		_top = top;
		_bottom = bottom;
		_scrollbar = scrollbar;
	}

	public void ShowInfo()
	{
		if (transform.position.y > _top)
		{
			Debug.Log("Higher");
			_scrollbar.value += 0.28f;
		}

		if (transform.position.y < _bottom)
		{
			Debug.Log("Lower");
			_scrollbar.value -= 0.28f;
		}

		
		GameObject.FindGameObjectWithTag("NotesQuestTitle").GetComponent<TextMeshProUGUI>().text = _quest.Name;
		StringBuilder obtained = new StringBuilder();
		int lastCompleted= 0;
		foreach (Objective objective in _quest.Objectives)
		{
			
			Debug.Log(objective.Description+"   "+objective.Completed);
			if (objective.Completed)
			{
				
				obtained.Append(objective.Description);
				Debug.Log(objective.Description);
				obtained.Append("\n");
				lastCompleted++;
			}
		}

		
		if(lastCompleted < _quest.Objectives.Count)
			obtained.Append(_quest.Objectives[lastCompleted].Description);
		
		GameObject.FindGameObjectWithTag("NotesQuestObjectiveDescription").GetComponent<TextMeshProUGUI>().text = obtained.ToString();
		
	}
}
