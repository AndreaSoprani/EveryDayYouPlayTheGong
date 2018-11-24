using UnityEngine;

namespace Quests.Objectives
{
    [System.Serializable]
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
            EventManager.StopListening("Play" + PlayableObject, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}