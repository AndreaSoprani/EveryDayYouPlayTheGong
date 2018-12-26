using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using UnityEngine.UI;

public class NotesListWindow : MonoBehaviour {

	public NotesListElementController ItemSlotPrefab;
	public GameObject Content;

	public Transform Top;
	public Transform Bottom;
	public Scrollbar Scrollbar;

	private NotesListElementController _firstElement;

	private void OnEnable()
	{
		UpdateItems();
		
	}


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
			newItem.SetQuest(todayQuest, Top.position.y, Bottom.position.y, Scrollbar);
			if (first)
			{
				_firstElement = newItem;
				_firstElement.GetComponent<Toggle>().Select();
				first = false;
			}
		}
		
		Scrollbar.value = 1;
	}


}
