using UnityEngine;

namespace Quests.Objectives
{
    [System.Serializable]
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
            EventManager.StopListening("Interact" + InteractiveObject, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}