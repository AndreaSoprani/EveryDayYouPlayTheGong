
using UnityEditor;
using Utility;

[CustomEditor (typeof(PathNode))]
public class PathNodeEditor : UnityEditor.Editor {

	public override void OnInspectorGUI()
    {
        PathNode myScript = (PathNode) target;

        myScript.Type = (PathNodeType) EditorGUILayout.EnumPopup("Node type",myScript.Type);
        if (myScript.Type == PathNodeType.Dialogue)
        {
            myScript.Text =(Dialogue) EditorGUILayout.ObjectField("Dialogue to be displayed", myScript.Text, typeof(Dialogue),true);
        }
        
    }
}
