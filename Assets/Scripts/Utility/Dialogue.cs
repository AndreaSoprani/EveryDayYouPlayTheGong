using System.Collections.Generic;
using System.Collections.ObjectModel;
using Quests;
using Quests.Objectives;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public TextAsset TextAsset; // The text to display.
        public string NPCName; // The name of the NPC to display.
        public List<Quest> QuestsToActivate; // List of quests to activate after the dialogue.
        public List<Item> ItemsToAdd; // List of the items to add after the dialogue.
        public List<Item> ItemsToRemove; // List of the items to remove after the dialogue.
        public List<Objective> ConditionsOnObjectives; // List of objectives that have to be accomplished in order to
                                                       // make this dialogue available.

        /// <summary>
        /// Calls the TextBoxManager and starts the dialogue.
        /// </summary>
        public void StartDialogue()
        {
            TextBoxManager.Instance.LoadDialogue(this);
            TextBoxManager.Instance.EnableTextBox();
        }

        /// <summary>
        /// Method used to check if a dialogue is available based on the objectives conditions.
        /// </summary>
        /// <returns>True if the dialogue can be performed, false otherwise</returns>
        public bool IsAvailable()
        {
            for (int i = 0; i < ConditionsOnObjectives.Count; i++)
            {
                if (!ConditionsOnObjectives[i].Completed) return false;
            }

            return true;
        }
    }
}