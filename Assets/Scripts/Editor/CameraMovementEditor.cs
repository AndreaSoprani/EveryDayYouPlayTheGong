using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(CameraMovement))]
public class CameraMovementEditor : UnityEditor.Editor {

	public override void OnInspectorGUI()
    {
        CameraMovement myScript = (CameraMovement) target;
       
        myScript.smoothTime = EditorGUILayout.Slider("Smooth", myScript.smoothTime, 0, 1);
        myScript.hasDeadZone = EditorGUILayout.Toggle("Dead zone", myScript.hasDeadZone);
        if(myScript.hasDeadZone)
        {
            myScript.marginX = EditorGUILayout.FloatField("Margin X", myScript.marginX);
            myScript.marginY = EditorGUILayout.FloatField("Margin Y", myScript.marginY);
        }

    }
}
