using System;
using UnityEngine;

namespace Quests.Objectives
{
    [System.Serializable]
    public class RemoveItemObjective : Objective
    {
        public string ItemId;
        
        //TODO add event when item is removed.
        
        public override void StartListening()
        {
            EventManager.StartListening("RemoveItem" + ItemId, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("RemoveItem" + ItemId, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("RemoveItem" + ItemId, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}