using System.Collections.Generic;
using System.Linq;
using Quests.Objectives;
using UnityEngine;

namespace Quests
{
    /// <summary>
    /// The Quest class contains the information for a single quest with all the relative objectives.
    /// </summary>
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {

        public string QuestID; // Unique ID of the Quest.
        public string Name;
        public string Description; // Text description of the Quest.
        public List<Objective> Objectives;
        public bool Completed;
        public List<Quest> ActivateWhenComplete; // List of quests to activate when this one is complete.

        /// <summary>
        /// Signals if all the objectives are completed.
        /// </summary>
        /// <returns>True if all objectives are completed, false if at least one is not.</returns>
        public bool IsComplete()
        {
            return Completed;
        }

        public void UpdateCompletion()
        {
            if (Completed) return;
            
            for (int i = 0; i < Objectives.Count(); i++)
            {
                if (!Objectives[i].Completed)
                {
                    Completed = false;
                    GameObject.FindGameObjectWithTag("GUIController").SendMessage("NotifyNewObjective", Objectives[i].Description);
                    return;
                }
            }

            Completed = true;
            
            Debug.Log("Quest " + QuestID + " Completed");
            
            QuestManager.Instance.QuestCompleted(this);
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
