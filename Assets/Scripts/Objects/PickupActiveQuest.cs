using System.Text;
using Quests;
using UnityEngine;

namespace Objects
{
    public class PickupActiveQuest : Pickup
    {
        public Quest ActiveQuest;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;
        private Animator _animator;

        private void Start()
        {
            StringBuilder nameBuilder = new StringBuilder("Pickup");
            foreach (Item item in Items)
            {
                nameBuilder.Append("-" + item.Id);
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            EventManager.StartListening("QuestActivated" + ActiveQuest.QuestID, Activate);
        }

        private void Activate()
        {
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            _animator.enabled = true;
        }
    }
}