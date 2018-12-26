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

            if (Dialogue != null)
            {
                Debug.Log(Dialogue.IsAvailable());
                if (Dialogue.IsAvailable())
                {
                    Dialogue.StartDialogue();
                    foreach (Quest quest in Dialogue.QuestsToActivate)
                    {
                        QuestManager.Instance.ActivateQuest(quest);
                    }
                    if (Dialogue.OneTimeDialogue)
                        Dialogue = null;
                }
                
            }
               
            

           
        }
    }
}