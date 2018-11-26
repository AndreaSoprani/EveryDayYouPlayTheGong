using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Interact Objective", menuName = "InteractObjective", order = 3)]
    public class InteractObjective : Objective
    {
        public IInteractiveObject InteractiveObject;
        
        public override void StartListening()
        {
            EventManager.StartListening("Interact" + InteractiveObject, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("Interact" + InteractiveObject, Complete);
        }

        public override void Complete()
        {
            Quest quest = QuestManager.GetQuest(QuestID);
            if (quest == null) return; 
            if (quest.NextObjective() != this) return;
            
            EventManager.StopListening("Interact" + InteractiveObject, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}