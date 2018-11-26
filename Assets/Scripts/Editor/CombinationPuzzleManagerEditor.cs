using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombinationPuzzleManager))]
public class CombinationPuzzleManagerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        CombinationPuzzleManager myScript = (CombinationPuzzleManager) target;
        myScript.PuzzleId = EditorGUILayout.TextField("Id", myScript.PuzzleId);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("PuzzlePiece"), true);
        myScript.TypeOfPuzzle = (PuzzleType) EditorGUILayout.EnumPopup("Puzzle type", myScript.TypeOfPuzzle);
        
        
        if(myScript.TypeOfPuzzle == PuzzleType.OpenDoor)
            myScript.DoorToBeOpened = (Door)EditorGUILayout.ObjectField("Door to be opened",myScript.DoorToBeOpened,typeof(Door),true);
        else if(myScript.TypeOfPuzzle == PuzzleType.ItemReward)

            myScript.Reward = (Pickup)EditorGUILayout.ObjectField("Reward",myScript.Reward,typeof(Pickup),true);

       
        serializedObject.ApplyModifiedProperties();
    }
}
         
