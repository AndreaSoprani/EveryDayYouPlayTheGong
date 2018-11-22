namespace Quests.Objectives
{
    [System.Serializable]
    public class RemoveItemObjective : Objective
    {
        public Item Item;
        
        //TODO add event when item is removed.
        
        public override void StartListening()
        {
            EventManager.StartListening("RemoveItem" + Item.Id, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("RemoveItem" + Item.Id, Complete);
        }

        public override void Complete()
        {
            EventManager.StopListening("RemoveItem" + Item.Id, Complete);
            this.Completed = true;
        }
    }
}