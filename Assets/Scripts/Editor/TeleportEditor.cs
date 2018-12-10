
using UnityEditor;
using Utility;

[CustomEditor (typeof(Teleport))]
 public class TeleportEditor : UnityEditor.Editor
{
 public override void OnInspectorGUI()
 {
  Teleport myScript = (Teleport) target;

  myScript.Destination = (Teleport) EditorGUILayout.ObjectField("Destination",myScript.Destination,typeof(Teleport),true);
  myScript.Offset = EditorGUILayout.Vector3Field("Offset", myScript.Offset);
  myScript.CanChangeMusic = EditorGUILayout.Toggle("Can change music", myScript.CanChangeMusic);
  
  if (myScript.CanChangeMusic)
  {
   myScript.MusicId =EditorGUILayout.TextField("Event name",myScript.MusicId);
  }
  
  EditorGUILayout.LabelField("Fade",EditorStyles.boldLabel);
  myScript.TimeIn = EditorGUILayout.FloatField("Time in",myScript.TimeIn);
  myScript.Pause=EditorGUILayout.FloatField("Pause",myScript.Pause);
  myScript.TimeOut=EditorGUILayout.FloatField("Time out",myScript.TimeOut);
 }
}
