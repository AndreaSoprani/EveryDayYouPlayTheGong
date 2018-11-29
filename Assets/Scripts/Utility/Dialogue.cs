using System.Collections.Generic;
using Quests;
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

        /// <summary>
        /// Calls the TextBoxManager and starts the dialogue.
        /// </summary>
        public void StartDialogue()
        {
            TextBoxManager.Instance.LoadDialogue(this);
            TextBoxManager.Instance.EnableTextBox();
        }

    }
}