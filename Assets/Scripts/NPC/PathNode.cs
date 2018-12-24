using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PathNode : MonoBehaviour
{

	public PathNodeType Type;
	public Dialogue Text;
	public float TimeToWait;
}
public enum PathNodeType
{
	Waypoint, Dialogue,Waiting
}
