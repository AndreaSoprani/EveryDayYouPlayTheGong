using Quests;
using UnityEngine;
using Utility;

namespace Objects
{
    public class ObjectWithDialogue : InGameObject
    {
        public Dialogue Dialogue;

        private void Start()
        {
            if(Dialogue!=null) Dialogue.ResetReproduced();
        }

        public override bool IsInteractable()
        {
            return Dialogue != null && Dialogue.IsAvailable();
        }

        public override void Interact()
        {
            if (Dialogue != null && Dialogue.IsAvailable())
            {
                Dialogue.StartDialogue();
            }
        }
    }
}