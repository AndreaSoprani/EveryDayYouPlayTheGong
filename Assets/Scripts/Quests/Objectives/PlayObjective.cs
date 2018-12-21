using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Play Objective", menuName = "PlayObjective", order = 4)]
    public class PlayObjective : Objective
    {
        public string ObjectID;

        public override void StartListening()
        {
            EventManager.StartListening("Play" + ObjectID, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("Play" + ObjectID, Complete);
        }

        public override void Complete()
        {
            Quest quest = QuestManager.Instance.GetQuest(QuestID);
            if (quest == null) return; 
            if (quest.NextObjective() != this) return;
            if (EventManager.IsBlocked("Play" + ObjectID)) return;
            
            EventManager.StopListening("Play" + ObjectID, Complete);
            EventManager.BlockEvent("Play" + ObjectID);
            base.Complete();
        }
    }
}