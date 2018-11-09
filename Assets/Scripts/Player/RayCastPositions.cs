using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class RayCastPositions
{

	public Transform UpLeft;
	public Transform UpRight;
	public Transform DownLeft;
	public Transform DownRight;

	/// <summary>
	/// Used to retrieve all the RayCast positions involved with a specific movement (represented by a Vector3)
	/// </summary>
	/// <param name="vec">The Vector3 involved with the movement/facing direction</param>
	/// <returns>A Collection which contains all the RayCast positions involved</returns>
	public Collection<Vector3> Vector3ToRayCastPosition(Vector3 vec)
	{
		Collection<Vector3> positions = new Collection<Vector3>();

		if (vec.y > 0)
		{
			positions.Add(UpLeft.position);
			positions.Add(UpRight.position);
		} else if (vec.y < 0)
		{
			positions.Add(DownLeft.position);
			positions.Add(DownRight.position);
		}

		if (vec.x > 0)
		{
			if(!positions.Contains(UpRight.position)) positions.Add(UpRight.position);
			if(!positions.Contains(DownRight.position)) positions.Add(DownRight.position);
		} else if (vec.x < 0)
		{
			if(!positions.Contains(UpLeft.position)) positions.Add(UpLeft.position);
			if(!positions.Contains(DownLeft.position)) positions.Add(DownLeft.position);
		}
		
		return positions;
	}

}
