using UnityEngine.Analytics;

namespace Quests.Objectives
{
    [System.Serializable]
    public class AddItemObjective : Objective
    {
        public Item Item;

        //TODO trigger event in player when item is added.

        public override void StartListening()
        {
            EventManager.StartListening("AddItem" + Item.Id, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("AddItem" + Item.Id, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("AddItem" + Item.Id, Complete);
            this.Completed = true;
        }
    }
}