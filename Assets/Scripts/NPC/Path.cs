using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Path : MonoBehaviour
{

	public List<Transform> WayPoints;
	public PathType Type;
	public Color LineColor=Color.green;
	
	private void OnDrawGizmos()
	{
		Gizmos.color=LineColor;
		if (WayPoints != null)
		{
			for (int i=0;i<WayPoints.Count-1;i++)
			{
				
				Gizmos.DrawLine(WayPoints[i].position,WayPoints[i+1].position);
			}
			if(Type == PathType.Loop)
				Gizmos.DrawLine(WayPoints[0].position,WayPoints[WayPoints.Count-1].position);
				
		}
	}

	private void OnTransformChildrenChanged()
	{
		WayPoints.Clear();
		Debug.Log("ok");
		for (int i = 0; i < transform.childCount; i++) 
			WayPoints.Add(transform.GetChild(i));
		OnDrawGizmos();
	}
}
