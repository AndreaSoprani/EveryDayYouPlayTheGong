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

        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            SetStick(HasStick);

            if(HasStick) EventManager.StartListening("Blackout", () => SetStick(false));
        }
        
        private void SetStick(bool hasStick)
        {
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