using System.Collections;
using System.Collections.Generic;
using System.Text;
using Objects;
using Quests;
using UnityEngine;
[ExecuteInEditMode]
public class Pickup : InGameObject
{

	public List<Item> Items;
	public List<Quest> QuestsToActivate;
	
	// Use this for initialization
	void Start ()
	{
		StringBuilder nameBuilder = new StringBuilder("Pickup");
		foreach (Item item in Items)
		{
			nameBuilder.Append("-" + item.Id);
		}
	}

	public override void Interact()
	{
		foreach(Item item in Items) Player.Instance.AddItem(item);
		foreach(Quest quest in QuestsToActivate) QuestManager.Instance.ActivateQuest(quest);
		Destroy(gameObject);
	}
}
