using System.Linq;
using UnityEngine;

namespace Quests.Objectives
{
    [CreateAssetMenu( fileName = "New Open Door Objective", menuName = "OpenDoorObjective", order = 6)]
    public class OpenDoorObjective : Objective
    {
        public string ObjectID;
        
        public override void StartListening()
        {
            Door door = FindObjectsOfType<Door>().ToList().Find(d => d.ObjectID == ObjectID);
            if (door.IsOpen)
            {
                base.Complete();
                return;
            }
            
            EventManager.StartListening("OpenDoor" + ObjectID, Complete);
        }

        public override void StopListening()
        {
            EventManager.StopListening("OpenDoor" + ObjectID, Complete);
        }

        public override void Complete()
        {   
            EventManager.StopListening("OpenDoor" + ObjectID, Complete);
            base.Complete();
        }
    }
}