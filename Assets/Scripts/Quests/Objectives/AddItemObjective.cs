using System;
using UnityEngine;

namespace Quests.Objectives
{
    [System.Serializable]
    public class AddItemObjective : Objective
    {
        public string ItemId;

        //TODO trigger event in player when item is added.

        public override void StartListening()
        {
            EventManager.StartListening("AddItem" + ItemId, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("AddItem" + ItemId, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("AddItem" + ItemId, Complete);
            this.Completed = true;
            Debug.Log("Objective " + this.ObjectiveID + " completed.");
        }
    }
}