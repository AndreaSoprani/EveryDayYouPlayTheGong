using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PathNode : MonoBehaviour
{

	public PathNodeType Type;
	public Dialogue Text;
}
public enum PathNodeType
{
	Waypoint, Dialogue
}
