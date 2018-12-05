using UnityEditor;
using Utility;

[CustomEditor (typeof(CombinationPuzzleObject))]
public class CombinationPuzzleObjectEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        CombinationPuzzleObject myScript = (CombinationPuzzleObject) target;

        myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);
        myScript.Sound = (Sound)  EditorGUILayout.ObjectField("Sound", myScript.Sound, typeof(Sound), true);

        myScript.HasInteraction = EditorGUILayout.Toggle("Has Interaction", myScript.HasInteraction);
        
        if (myScript.HasInteraction)
        {
            myScript.Dialogue = (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.Dialogue, typeof(Dialogue), true);
        }

    }
}