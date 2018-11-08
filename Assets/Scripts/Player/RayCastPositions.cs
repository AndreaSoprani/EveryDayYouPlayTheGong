using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RayCastPositions
{

	public Transform Up;
	public Transform Down;
	public Transform Left;
	public Transform Right;

	/// <summary>
	/// Returns the RayCastPosition given a standard Vector3 (up, down, left and right).
	/// </summary>
	/// <param name="vec">Standard 2D Vector3 (up, down, left, right)</param>
	/// <returns>The corresponding RayCast position as a transform. Default is up</returns>
	public Transform Vector3ToRaycastPosition(Vector3 vec)
	{
		if (vec == Vector3.down) return Down;
		if (vec == Vector3.right) return Right;
		if (vec == Vector3.left) return Left;
		return Up;
	}

}
