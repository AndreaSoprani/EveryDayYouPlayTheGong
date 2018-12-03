using System.Collections;
using System.Collections.Generic;
using System.Text;
using Quests;
using Quests.Objectives;
using UnityEngine;
using UnityEngine.UI;

public class NotesListElementController : MonoBehaviour {

	private Quest _quest;
	
	public Text NameField;

	private void Start()
	{
		
		NameField.text = _quest.Name;
	}

	public void SetQuest(Quest quest)
	{
		_quest = quest;
		NameField.text = _quest.Name;
	}

	public void ShowInfo()
	{
		GameObject.FindGameObjectWithTag("NotesQuestTitle").GetComponent<Text>().text = _quest.Name;
		StringBuilder obtained = new StringBuilder();
		int lastCompleted= 0;
		foreach (Objective objective in _quest.Objectives)
		{
			if (objective.Completed)
			{
				obtained.Append(objective.Description);
				obtained.Append("\n");
				lastCompleted++;
			}
		}

		if (lastCompleted == 0)
		{
			obtained.Append(_quest.Objectives[lastCompleted].Description);
		}
		else
		{
			obtained.Append(_quest.Objectives[lastCompleted+1].Description);
		}
		
		GameObject.FindGameObjectWithTag("NotesQuestObjectiveDescription").GetComponent<Text>().text = obtained.ToString();
		
	}
}
