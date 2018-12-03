using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using UnityEngine.UI;

public class NotesListWindow : MonoBehaviour {

	public NotesListElementController ItemSlotPrefab;
	public GameObject Content;


	private NotesListElementController _firstElement;

	

	public void UpdateItems()
	{
	
		foreach (Transform child in Content.transform)
		{
			Destroy(child.gameObject);
			
		}

		bool first = true;
		foreach (Quest todayQuest in QuestManager.Instance.TodayQuests)
		{
			
			NotesListElementController newItem = Instantiate(ItemSlotPrefab);
			newItem.transform.SetParent(Content.transform);
			newItem.SetQuest(todayQuest);
			if (first)
			{
				_firstElement = newItem;
				_firstElement.GetComponent<Toggle>().Select();
				first = false;
			}
		}
	}


}
