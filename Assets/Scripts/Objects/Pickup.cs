using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
[ExecuteInEditMode]
public class Pickup : InGameObject
{

	public Item Item;
	
	// Use this for initialization
	void Start ()
	{
		if (Item == null) return;
		this.name = "Pickup" + Item.Id;
	}

	public override void Interact()
	{
		if (Item != null) Player.Instance.AddItem(Item);
		Destroy(gameObject);
	}
}
