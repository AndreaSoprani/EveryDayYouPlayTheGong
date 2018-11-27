using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Path : MonoBehaviour
{

	public List<PathNode> WayPoints;
	public PathType Type;
	public Color LineColor=Color.green;
	
	private void OnDrawGizmos()
	{
		Gizmos.color=LineColor;
		if (WayPoints != null)
		{
			for (int i=0;i<WayPoints.Count-1;i++)
			{
				
				Gizmos.DrawLine(WayPoints[i].transform.position,WayPoints[i+1].transform.position);
			}
			if(Type == PathType.Loop)
				Gizmos.DrawLine(WayPoints[0].transform.position,WayPoints[WayPoints.Count-1].transform.position);
				
		}
	}

	
}
