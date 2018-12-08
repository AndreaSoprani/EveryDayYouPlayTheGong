using System.Configuration;
using Objects;
using UnityEditor;
using Utility;

[CustomEditor (typeof(MainGong))]
public class MainGongEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MainGong myScript = (MainGong) target;

        myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);
        myScript.SoundName = EditorGUILayout.TextField("Sound Name", myScript.SoundName);

        myScript.HasInteraction = EditorGUILayout.Toggle("Has Interaction", myScript.HasInteraction);
        
        if (myScript.HasInteraction)
        {
            myScript.Dialogue = (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.Dialogue, typeof(Dialogue), true);
        }

        myScript.WrongTimeToPlayDialogue = (Dialogue) EditorGUILayout.ObjectField("Wrong time to play dialogue",
            myScript.WrongTimeToPlayDialogue, typeof(Dialogue), true);

    }
}