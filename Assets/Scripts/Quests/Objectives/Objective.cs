using UnityEngine;

namespace Quests.Objectives
{
    /// <summary>
    /// The Objective class contains the information for a single objective inside a quest.
    /// </summary>
    public abstract class Objective : ScriptableObject
    {
        public string ObjectiveID; // Unique ID of the Objective.
        public string QuestID; // QuestID of the quest containing this Objective.
        public string Description; // Text description of the objective.
        public bool Completed;

        /// <summary>
        /// Starts to listen on a specific event based on the objective type.
        /// </summary>
        public abstract void StartListening();
        
        /// <summary>
        /// Stops listening on the objective event.
        /// </summary>
        public abstract void StopListening();
        
        /// <summary>
        /// Method that must be called when the event associated to the objective is triggered.
        /// Stops listening on the event and sets Completed to true.
        /// </summary>
        public virtual void Complete()
        {
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
            QuestManager.Instance.GetQuest(QuestID).UpdateCompletion();
        }
    }
}
