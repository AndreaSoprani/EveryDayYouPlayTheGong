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
        [Header("Text info and asset")]
        public TextAsset TextAsset; // The text to display.
        public string NPCName; // The name of the NPC to display.
        
        [Header("Condition on quest and objectives")]
        public Quest ActiveQuest; // The quest that must be active in order to use this dialogue.
        public List<Objective> CompletedObjectives; // List of objectives that have to be accomplished in order to
        // make this dialogue available.
        
        [Header("Consequences of the dialogue")]
        public List<Quest> QuestsToActivate; // List of quests to activate after the dialogue.
        public List<Item> ItemsToAdd; // List of the items to add after the dialogue.
        public List<Item> ItemsToRemove; // List of the items to remove after the dialogue.
        

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

            if (ActiveQuest != null &&
                !QuestManager.Instance.GetQuest(ActiveQuest.QuestID).Active)
            {
                return false;
            }
            
            for (int i = 0; i < CompletedObjectives.Count; i++)
            {
                if (!CompletedObjectives[i].Completed) return false;
            }

            return true;
        }
    }
}