using System.Configuration;
using Objects;
using UnityEditor;
using UnityEngine;
using Utility;

[CustomEditor (typeof(MainGong))]
public class MainGongEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MainGong myScript = (MainGong) target;

        myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SoundName"), true);
        
        myScript.SoundDelay = EditorGUILayout.Slider("Sound Delay", myScript.SoundDelay, 0, 1);
        myScript.AnimationDelay = EditorGUILayout.Slider("Animation Delay", myScript.AnimationDelay, 0, 1);

        myScript.HasInteraction = EditorGUILayout.Toggle("Has Interaction", myScript.HasInteraction);
        
        if (myScript.HasInteraction)
        {
            myScript.Dialogue = (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.Dialogue, typeof(Dialogue), true);
        }

        myScript.WrongTimeToPlayDialogue = (Dialogue) EditorGUILayout.ObjectField("Wrong time to play dialogue",
            myScript.WrongTimeToPlayDialogue, typeof(Dialogue), true);

        myScript.NewDayText = (TextAsset) EditorGUILayout.ObjectField("New day text", myScript.NewDayText, typeof(TextAsset), true);
        serializedObject.ApplyModifiedProperties();
    }
}