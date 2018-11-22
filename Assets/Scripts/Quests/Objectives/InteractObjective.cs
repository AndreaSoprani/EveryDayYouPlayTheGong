namespace Quests.Objectives
{
    [System.Serializable]
    public class InteractObjective : Objective
    {
        public IInteractiveObject InteractiveObject;

        //TODO trigger event in player when interaction occurs.

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
        }
    }
}