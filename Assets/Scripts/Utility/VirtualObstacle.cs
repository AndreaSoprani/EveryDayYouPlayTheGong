using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class VirtualObstacle : MonoBehaviour
{
	public string LimitId;
	public Dialogue Warning;
	public NPCFacing NPC;
	public float Step;
	


	private void Start()
	{
		EventManager.StartListening("NPCEnterDialogue"+NPC.ID, DestroyEverything);
		EventManager.StartListening("NPCExitDialogue"+LimitId, ReplacePlayer);
	}

	private void ReplacePlayer()
	{
		Player.Instance.InvertFacing(Step);
	}

	private void DestroyEverything()
	{
		
		EventManager.StopListening("NPCEnterDialogue"+NPC.ID, DestroyEverything);
		EventManager.StopListening("ExitDialogueLimit1"+LimitId, ReplacePlayer);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.CompareTag("Player"))
		{
			
			NPC.ChangeFacing(Player.Instance.transform.position - NPC.transform.position);
			Warning.StartDialogue(LimitId);
			
			
			
		}
	}

	
}
