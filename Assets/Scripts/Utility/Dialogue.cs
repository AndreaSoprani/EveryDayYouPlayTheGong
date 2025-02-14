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

        [Header("Dialogue settings")] 
        public bool DontChangeNPCFacing; // If true the NPCs will not face the player when the dialogue starts.
        public bool OneTimeDialogue; // If true the dialogue can be reproduced just one time.
        
        [Header("Conditions")]
        public Quest ActiveQuest; // The quest that must be active in order to use this dialogue.
        public List<Objective> CompletedObjectives; // List of objectives that have to be accomplished in order to
        // make this dialogue available.
        public List<Quest> CompletedQuests; // List of quests that must be completed to make this dialogue available.
        public List<Quest> UncompletedQuests; // List of quests that must be not completed to make this dialogue available.
        public List<Item> NecessaryItems;
        
        [Header("Consequences of the dialogue")]
        public List<Quest> QuestsToActivate; // List of quests to activate after the dialogue.
        public List<Item> ItemsToAdd; // List of the items to add after the dialogue.
        public List<Item> ItemsToRemove; // List of the items to remove after the dialogue.
        
        private bool _hasBeenReproduced = false;

        /// <summary>
        /// Calls the TextBoxManager and starts the dialogue.
        /// </summary>
        public void StartDialogue()
        {
            if (OneTimeDialogue && _hasBeenReproduced) return;
            TextBoxManager.Instance.LoadDialogue(this);
            TextBoxManager.Instance.EnableTextBox();
            _hasBeenReproduced = true;
        }
        /// <summary>
        /// Calls the TextBoxManager and starts the dialogue from NPC.
        /// </summary>
        public void StartDialogue(string NPCid)
        {
            if (OneTimeDialogue && _hasBeenReproduced) return;
            TextBoxManager.Instance.LoadDialogue(this);
            TextBoxManager.Instance.EnableTextBox(NPCid);
            _hasBeenReproduced = true;
        }
        

        /// <summary>
        /// Method used to check if a dialogue is available based on the objectives conditions.
        /// </summary>
        /// <returns>True if the dialogue can be performed, false otherwise</returns>
        public bool IsAvailable()
        {
            if (OneTimeDialogue && _hasBeenReproduced) return false;

            if (ActiveQuest != null &&
                (!QuestManager.Instance.TodayQuests.Contains(ActiveQuest) ||
                 ActiveQuest.Completed))
            {
                return false;
            }
            
            for (int i = 0; i < CompletedObjectives.Count; i++)
            {
                if (!CompletedObjectives[i].Completed) return false;
            }

            for (int i = 0; i < CompletedQuests.Count; i++)
            {
                if (!CompletedQuests[i].Completed) return false;
            }
            
            for (int i = 0; i < UncompletedQuests.Count; i++)
            {
                if (UncompletedQuests[i].Completed) return false;
            }

            for (int i = 0; i < NecessaryItems.Count; i++)
            {
                if (!Player.Instance.HasItem(NecessaryItems[i])) return false;
            }
            
            return true;
        }

        public void ResetReproduced()
        {
            _hasBeenReproduced = false;
        }

        /// <summary>
        /// Checks whether a dialogue can start a new quest.
        /// </summary>
        /// <returns>True if a new quest can be activated, false otherwise</returns>
        public bool CanStartQuest()
        {
            return QuestsToActivate.Count > 0;
        }
    }
}