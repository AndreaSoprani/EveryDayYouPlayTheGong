using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class VirtualObstacle : MonoBehaviour
{
	public Dialogue Warning;
	public NPCFacing NPC;


	private void Start()
	{
		EventManager.StartListening("NPCEnterDialogue"+NPC.ID, DestroyEverything);
		
	}

	private void DestroyEverything()
	{
		Destroy(this);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Nope");
		if (other.gameObject.CompareTag("Player"))
		{
			
			NPC.ChangeFacing(Player.Instance.transform.position - NPC.transform.position);
			Warning.StartDialogue();	
			EventManager.StopListening("NPCEnterDialogue"+NPC.ID, DestroyEverything);
			
		}
	}

	
}
