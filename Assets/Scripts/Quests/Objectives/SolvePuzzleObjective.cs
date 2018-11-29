using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Puzzle Objective", menuName = "SolvePuzzleObjective", order = 5)]
    public class SolvePuzzleObjective: Objective
    {
        public string PuzzleId;

        public override void StartListening()
        {
            EventManager.StartListening("PuzzleSolved" + PuzzleId, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("PuzzleSolved" + PuzzleId, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("PuzzleSolved" + PuzzleId, Complete);
            base.Complete();
        }
    }
}