using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInstrument : MonoBehaviour
{

	[Header("Area")]
	public float X;
	public float Y;
	public Color AreaColor=Color.blue;
	//[Header("Sound")]
	
	
	private void OnDrawGizmos()
	{
		
		Gizmos.color=AreaColor;
		Gizmos.DrawLine(new Vector3(transform.position.x-X,transform.position.y+Y),new Vector3(transform.position.x+X,transform.position.y+Y));
		Gizmos.DrawLine(new Vector3(transform.position.x+X,transform.position.y+Y),new Vector3(transform.position.x+X,transform.position.y-Y));
		Gizmos.DrawLine(new Vector3(transform.position.x+X,transform.position.y-Y),new Vector3(transform.position.x-X,transform.position.y-Y));
		Gizmos.DrawLine(new Vector3(transform.position.x-X,transform.position.y-Y),new Vector3(transform.position.x-X,transform.position.y+Y));
	}
	
	
}
