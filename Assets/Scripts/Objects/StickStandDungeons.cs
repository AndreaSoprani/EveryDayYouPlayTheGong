using UnityEngine;
using Utility;

namespace Objects
{
    public class StickStandDungeons : ObjectWithDialogue
    {
        public bool HasStick;

        public Sprite NoStickSprite;
        public Sprite StickSprite;

        public Settings Settings;

        public Vector3 TeleportAfterInteraction;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (!HasStick)
            {
                _spriteRenderer.sprite = NoStickSprite;
                EventManager.StartListening("Blackout", SetStick);
            }
            else _spriteRenderer.sprite = StickSprite;
            
        }

        public override bool IsInteractable()
        {
            if (!HasStick) return base.IsInteractable();
            return true;
        }

        public override void Interact()
        {
            if (!HasStick) base.Interact();
            else
            {
                // Display Text
                if (Settings.EndText != null)
                {
                    GameObject.FindGameObjectWithTag("GUIController").SendMessage("DisplayTextForTime", Settings.EndText.text);
                }
                
                // Teleport
                Player.Instance.BlockMovement(true);
                Player.Instance.transform.position = TeleportAfterInteraction;
                GameObject.FindGameObjectWithTag("MainCamera").SendMessage("TeleportCamera");
                AudioManager.Instance.PlayMusic("Explore");
                Player.Instance.BlockMovement(false);
            }
        }

       

        private void SetStick()
        {
            HasStick = true;
            _spriteRenderer.sprite = StickSprite;
        }
    }
}