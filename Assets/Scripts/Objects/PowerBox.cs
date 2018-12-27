using System.Collections;
using UnityEngine;
using Utility;

namespace Objects
{
    public class PowerBox : InGameObject
    {
        public Sprite NormalSprite;
        public Sprite BrokenSprite;

        public Dialogue NormalDialogue;
        public Dialogue BrokenDialogue;
        public Dialogue CurrentBackOnDialogue;

        [Range(0,10)] public float BlackOutTime;

        public bool Broken;

        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (Broken) _spriteRenderer.sprite = BrokenSprite;
            else _spriteRenderer.sprite = NormalSprite;
        }

        public override bool IsInteractable()
        {
            if (Broken) return BrokenDialogue != null;
            else return NormalDialogue != null;
        }

        public override void Interact()
        {
            if(Broken) BrokenDialogue.StartDialogue();
            else NormalDialogue.StartDialogue();
        }

        public override bool IsPlayable()
        {
            return !Broken;
        }

        public override void Play()
        {
            StartCoroutine(BlackOut());
        }

        private IEnumerator BlackOut()
        {
            yield return new WaitForSeconds(0.4f); // Playing animation.
            
            CameraFade.Instance.InstantChange(1f);
            
            Broken = true;
            _spriteRenderer.sprite = BrokenSprite;
            EventManager.TriggerEvent("Blackout");
            
            yield return new WaitForSeconds(BlackOutTime);
            
            CameraFade.Instance.InstantChange(0f);
            
            if(CurrentBackOnDialogue != null) CurrentBackOnDialogue.StartDialogue();
        }
    }
}