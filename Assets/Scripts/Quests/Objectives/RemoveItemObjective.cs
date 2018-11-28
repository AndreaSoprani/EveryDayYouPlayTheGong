using System;
using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu(fileName = "New Remove Item Objective", menuName = "RemoveItemObjective", order = 2)]
    public class RemoveItemObjective : Objective
    {
        public string ItemId;
        
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
            base.Complete();
        }
    }
}