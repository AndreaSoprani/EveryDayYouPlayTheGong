using UnityEditor;
using UnityEngine;
using Utility;

[CustomEditor(typeof(CombinationPuzzleManager))]
public class CombinationPuzzleManagerEditor: UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        CombinationPuzzleManager myScript = (CombinationPuzzleManager) target;
        myScript.PuzzleId = EditorGUILayout.TextField("Id", myScript.PuzzleId);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("PuzzlePiece"), true);
        myScript.TypeOfPuzzle = (PuzzleType) EditorGUILayout.EnumPopup("Puzzle type", myScript.TypeOfPuzzle);
        myScript.HasTimeout = EditorGUILayout.Toggle("Timeout", myScript.HasTimeout);

        if (myScript.HasTimeout)
            myScript.TimeoutInSeconds = EditorGUILayout.FloatField("Timeout in seconds", myScript.TimeoutInSeconds);
        
        if(myScript.TypeOfPuzzle == PuzzleType.OpenDoor)
            myScript.DoorToBeOpened = (Door)EditorGUILayout.ObjectField("Door to be opened",myScript.DoorToBeOpened,typeof(Door),true);
        else if(myScript.TypeOfPuzzle == PuzzleType.ItemReward)

            myScript.Reward = (Pickup)EditorGUILayout.ObjectField("Reward",myScript.Reward,typeof(Pickup),true);
        else if (myScript.TypeOfPuzzle == PuzzleType.Dialogue)
        {
            myScript.Target=(NPCInteractable)EditorGUILayout.ObjectField("Target",myScript.Target,typeof(NPCInteractable),true);
            myScript.DialogueUnlocked =
                (Dialogue) EditorGUILayout.ObjectField("Dialogue", myScript.DialogueUnlocked, typeof(Dialogue), true);
            myScript.InstantTrigger = EditorGUILayout.Toggle("Has to instant trigger", myScript.InstantTrigger);
            if (myScript.InstantTrigger)
            {
                myScript.AlwaysEnable= EditorGUILayout.Toggle("Puzzle always enabled", myScript.AlwaysEnable);
            }
        }

        myScript.Silent = EditorGUILayout.Toggle("Silent", myScript.Silent);
       
        serializedObject.ApplyModifiedProperties();
    }
}
         
