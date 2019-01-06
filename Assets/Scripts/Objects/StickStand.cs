using Quests;
using UnityEngine;
using Utility;

namespace Objects
{
    public class StickStand : ObjectWithDialogue
    {
        public bool HasStick;

        public Sprite NoStickSprite;
        public Sprite StickSprite;

        public Dialogue NoStickDialogue;
        public Dialogue StickDialogue;

        public Quest BlackoutQuest;

        private SpriteRenderer _spriteRenderer;

        private bool isListening;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            isListening = false;
            
            SetStick(HasStick);

            if(HasStick) EventManager.StartListening("Blackout", () => SetStick(false));

            isListening = true;
        }
        
        private void SetStick(bool hasStick)
        {
            if (isListening)
            {
                if (!QuestManager.Instance.TodayQuests.Contains(BlackoutQuest)) return;
                EventManager.StopListening("Blackout", () => SetStick(false));
            }
            
            if (hasStick)
            {
                _spriteRenderer.sprite = StickSprite;
                Dialogue = StickDialogue;
            }
            else
            {
                _spriteRenderer.sprite = NoStickSprite;
                Dialogue = NoStickDialogue;
            }
        }
    }
}