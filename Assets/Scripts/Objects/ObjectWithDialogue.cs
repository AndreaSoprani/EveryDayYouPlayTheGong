using Utility;

namespace Objects
{
    public class ObjectWithDialogue : InGameObject
    {
        public Dialogue Dialogue;
        
        public override bool IsInteractable()
        {
            return Dialogue != null;
        }

        public override void Interact()
        {
            Dialogue.StartDialogue();
        }
    }
}