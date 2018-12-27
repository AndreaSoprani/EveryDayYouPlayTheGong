using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class AreaInstrument : InGameObject
{

	[Header("Area")]
	public float X=1;
	public float Y=1;
	public Color AreaColor=Color.blue;
	public Vector3 Offset=Vector3.zero; 	
	[Header("Sound")]
	public string SoundName;
	public float SoundDelay = 0.12f;
	

	private bool _isPlaying=false;
	
	private void OnDrawGizmos()
	{
		
		Gizmos.color=AreaColor;
		Gizmos.DrawLine(new Vector3(transform.position.x+Offset.x-X,transform.position.y+Offset.y+Y),new Vector3(transform.position.x+Offset.x+X,transform.position.y+Offset.y+Y));
		Gizmos.DrawLine(new Vector3(transform.position.x+Offset.x+X,transform.position.y+Offset.y+Y),new Vector3(transform.position.x+Offset.x+X,transform.position.y+Offset.y-Y));
		Gizmos.DrawLine(new Vector3(transform.position.x+Offset.x+X,transform.position.y+Offset.y-Y),new Vector3(transform.position.x+Offset.x-X,transform.position.y+Offset.y-Y));
		Gizmos.DrawLine(new Vector3(transform.position.x+Offset.x-X,transform.position.y+Offset.y-Y),new Vector3(transform.position.x+Offset.x-X,transform.position.y+Offset.y+Y));
	}
	

	public override void Interact()
	{
		if(!_isPlaying)StartCoroutine(DJDropTheBeat());
	}

	private IEnumerator DJDropTheBeat()
	{
		_isPlaying = true;
		yield return new WaitForSeconds(SoundDelay);
		AkSoundEngine.PostEvent(SoundName, gameObject,(uint) AkCallbackType.AK_EndOfEvent, callback, null);
		
	}

	private void callback(object o,AkCallbackType t,AkCallbackInfo i)
	{

		_isPlaying = false;
	}
	private void Update()
	{
		if (_isPlaying)
		{
			if(!PlayerInArea())
			{
				AudioManager.Instance.StopEvent(SoundName,1);

				_isPlaying = false;
			}

		}
	}

	private bool PlayerInArea()
	{
		return !(Player.Instance.transform.position.x > transform.position.x +Offset.x + X ||
		       Player.Instance.transform.position.x < transform.position.x +Offset.x - X ||
		       Player.Instance.transform.position.y > transform.position.y +Offset.y + Y ||
		       Player.Instance.transform.position.y < transform.position.y +Offset.y - Y);
	}
}
