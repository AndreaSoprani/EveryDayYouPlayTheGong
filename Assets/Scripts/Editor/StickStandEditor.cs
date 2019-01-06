using Objects;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Editor
{
    [CustomEditor(typeof(StickStand))]
    public class StickStandEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            StickStand myScript = (StickStand) target;
            
            myScript.ObjectID = EditorGUILayout.TextField("ID", myScript.ObjectID);

            myScript.HasStick = EditorGUILayout.Toggle("Has Stick", myScript.HasStick);

            EditorGUILayout.LabelField("Sprites", EditorStyles.boldLabel);
            
            myScript.NoStickSprite =
                (Sprite) EditorGUILayout.ObjectField("No Stick Sprite", myScript.NoStickSprite, typeof(Sprite), true);
            myScript.StickSprite =
                (Sprite) EditorGUILayout.ObjectField("Stick Sprite", myScript.StickSprite, typeof(Sprite), true);
            
            EditorGUILayout.LabelField("Dialogues", EditorStyles.boldLabel);
            
            myScript.NoStickDialogue =
                (Dialogue) EditorGUILayout.ObjectField("No Stick Dialogue", myScript.NoStickDialogue, typeof(Dialogue), true);
            myScript.StickDialogue =
                (Dialogue) EditorGUILayout.ObjectField("Stick Dialogue", myScript.StickDialogue, typeof(Dialogue), true);

            myScript.DestroyAfterDialogue = false;
            myScript.Dialogue = null;
        }
    }
}