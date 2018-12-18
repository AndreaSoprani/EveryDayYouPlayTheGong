using UnityEditor;
using Utility;

[CustomEditor (typeof(CombinationPuzzleObject))]
public class CombinationPuzzleObjectEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        CombinationPuzzleObject myScript = (CombinationPuzzleObject) target;

        myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SoundName"), true);

        myScript.HasInteraction = EditorGUILayout.Toggle("Has Interaction", myScript.HasInteraction);
        
        if (myScript.HasInteraction)
        {
            myScript.Dialogue = (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.Dialogue, typeof(Dialogue), true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}