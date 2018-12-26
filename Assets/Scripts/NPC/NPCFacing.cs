using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFacing : MonoBehaviour
{
	public int DefaultFacingDirection = 2;
	
	private Animator _animator;
	public string ID;
	private void OnValidate()
	{
		if (DefaultFacingDirection < 0 || DefaultFacingDirection > 3)
			DefaultFacingDirection = 2;
	}
	
	private void Start()
	{
		_animator = GetComponent<Animator>();
		_animator.SetInteger("Direction", DefaultFacingDirection);
		EventManager.StartListening("NPCEnterDialogue"+ID, EnterDialogue);
	}
	
	/// <summary>
	/// Changes the sprites of the NPC based on the direction
	/// </summary>
	/// <param name="direction">Direction the NPC should face</param>
	protected void ChangeFacing(Vector3 direction)
	{
		float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
		
		if (angle > -45f && angle < 45f)
		{
			_animator.SetInteger("Direction", 1);
		}
		else if (angle >= 45f && angle <= 135f)
		{
			_animator.SetInteger("Direction", 0);
		}
		else if (angle > 135f || angle < -135f)
		{
			_animator.SetInteger("Direction", 3);
		}
		else
		{
			_animator.SetInteger("Direction", 2);
		}
	}
	
	/**
	* Used to enter in dialogue mode.
	* Movement is disabled.
	*/
	public void EnterDialogue()
	{
		ChangeFacing(Player.Instance.transform.position - transform.position);
		
		EventManager.StopListening("NPCEnterDialogue"+ID, EnterDialogue);
		EventManager.StartListening("NPCExitDialogue"+ID, ExitDialogue);
	}
	
	/**
	* Used to exit dialogue mode.
	* Movement is re-enabled.
	*/
	public void ExitDialogue()
	{
		_animator.SetInteger("Direction", DefaultFacingDirection);
		
		EventManager.StopListening("NPCExitDialogue"+ID, ExitDialogue);
		EventManager.StartListening("NPCEnterDialogue"+ID, EnterDialogue);
	}
}
