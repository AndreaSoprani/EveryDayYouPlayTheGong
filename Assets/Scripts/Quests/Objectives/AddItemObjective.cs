using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu(fileName = "New Add Item Objective", menuName = "AddItemObjective", order = 1)]
    public class AddItemObjective : Objective
    {
        public string ItemId;

        public override void StartListening()
        {
            if (Player.Instance.HasItem(ItemId))
            {
                base.Complete();
                return;
            }
            
            EventManager.StartListening("AddItem" + ItemId, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("AddItem" + ItemId, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("AddItem" + ItemId, Complete);
            base.Complete();
        }
    }
}