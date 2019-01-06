using Puzzles;
using Quests;
using UnityEditor;
using Utility;

[CustomEditor (typeof(CombinationPuzzleObjectActiveQuest))]
public class CombinationPuzzleObjectActiveQuestEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        CombinationPuzzleObjectActiveQuest myScript = (CombinationPuzzleObjectActiveQuest) target;

        myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SoundName"), true);
        
        myScript.SoundDelay = EditorGUILayout.Slider("Sound Delay", myScript.SoundDelay, 0, 1);
        myScript.AnimationDelay = EditorGUILayout.Slider("Animation Delay", myScript.AnimationDelay, 0, 1);

        myScript.HasInteraction = EditorGUILayout.Toggle("Has Interaction", myScript.HasInteraction);
        
        if (myScript.HasInteraction)
        {
            myScript.Dialogue = (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.Dialogue, typeof(Dialogue), true);
        }

        myScript.ActiveQuest =
            (Quest) EditorGUILayout.ObjectField("Active Quest", myScript.ActiveQuest, typeof(Quest), true);
        
        serializedObject.ApplyModifiedProperties();
    }
}