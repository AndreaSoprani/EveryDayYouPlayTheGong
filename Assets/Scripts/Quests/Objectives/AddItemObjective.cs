using System;
using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu(fileName = "New Add Item Objective", menuName = "AddItemObjective", order = 1)]
    public class AddItemObjective : Objective
    {
        public string ItemId;
        
        // TODO check if player already has the item

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