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
		
		GameObject.FindGameObjectWithTag("NotesQuestObjectiveDescription").GetComponent<Text>().text = obtained.ToString();
		
	}
}
