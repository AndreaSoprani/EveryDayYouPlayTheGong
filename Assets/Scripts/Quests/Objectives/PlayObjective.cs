using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Play Objective", menuName = "PlayObjective", order = 4)]
    public class PlayObjective : Objective
    {
        public IPlayableObject PlayableObject;

        public override void StartListening()
        {
            EventManager.StartListening("Play" + PlayableObject, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("Play" + PlayableObject, Complete);
        }

        public override void Complete()
        {
            Quest quest = QuestManager.GetQuest(QuestID);
            if (quest == null) return; 
            if (quest.NextObjective() != this) return;
            
            EventManager.StopListening("Play" + PlayableObject, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}