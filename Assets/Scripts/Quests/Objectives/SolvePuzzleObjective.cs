using System.Linq;
using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Puzzle Objective", menuName = "SolvePuzzleObjective", order = 5)]
    public class SolvePuzzleObjective: Objective
    {
        public string PuzzleId;

        public override void StartListening()
        {
            CombinationPuzzleManager puzzle = FindObjectsOfType<CombinationPuzzleManager>().ToList()
                .Find(c => c.PuzzleId == PuzzleId);
            if (puzzle != null && puzzle.IsSolved())
            {
                Complete();
                return;
            }
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