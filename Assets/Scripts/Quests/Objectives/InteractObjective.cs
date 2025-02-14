using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Interact Objective", menuName = "InteractObjective", order = 3)]
    public class InteractObjective : Objective
    {
        public string ObjectID;
        
        public override void StartListening()
        {
            EventManager.StartListening("Interact" + ObjectID, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("Interact" + ObjectID, Complete);
        }

        public override void Complete()
        {
            Quest quest = QuestManager.Instance.GetQuest(QuestID);
            if (quest == null) return; 
            if (quest.NextObjective() != this) return;
            if (EventManager.IsBlocked("Interact" + ObjectID)) return;
            
            EventManager.StopListening("Interact" + ObjectID, Complete);
            EventManager.BlockEvent("Interact" + ObjectID);
            base.Complete();
        }
    }
}