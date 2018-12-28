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
		StringBuilder questName = new StringBuilder();
		questName.Append(_quest.Name);
		if (_quest.Completed) questName.Append(" [COMPLETED]");
		NameField.text = questName.ToString();

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
		foreach (Objective objective in _quest.Objectives)
		{
			if (objective.Completed)
			{
				
				obtained.Append("[x] " + objective.Description);
				obtained.Append("\n");
			}
			else
			{
				obtained.Append("[ ] " + objective.Description);
				obtained.Append("\n");
				break;
			}
		}
		
		GameObject.FindGameObjectWithTag("NotesQuestObjectiveDescription").GetComponent<TextMeshProUGUI>().text = obtained.ToString();
		
	}
}
