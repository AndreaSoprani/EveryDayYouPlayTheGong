using System.Collections.Generic;
using System.Linq;
using Quests.Objectives;

namespace Quests
{
    /// <summary>
    /// The Quest class contains the information for a single quest with all the relative objectives.
    /// </summary>
    [System.Serializable]
    public class Quest
    {

        public string QuestID; // Unique ID of the Quest.
        public string Description; // Text description of the Quest.
        public List<Objective> Objectives;

        /// <summary>
        /// Signals if all the objectives are completed.
        /// </summary>
        /// <returns>True if all objectives are completed, false if at least one is not.</returns>
        public bool IsComplete()
        {
            for (int i = 0; i < Objectives.Count(); i++)
            {
                if (!Objectives[i].Completed) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the first uncompleted objective.
        /// </summary>
        /// <returns>The first uncompleted objective, null if they are all complete.</returns>
        public Objective NextObjective()
        {
            for (int i = 0; i < Objectives.Count; i++)
            {
                if (!Objectives[i].Completed) return Objectives[i];
            }

            return null;
        }
    }
}
