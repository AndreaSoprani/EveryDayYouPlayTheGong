using Quests;

namespace Puzzles
{
    public class CombinationPuzzleObjectActiveQuest : CombinationPuzzleObject
    {
        public Quest ActiveQuest;

        private bool _questActivated;

        private void Start()
        {
            _questActivated = false;
            EventManager.StartListening("QuestActivated" + ActiveQuest.QuestID, Activate);
            
        }

        public override void Play()
        {
            if(_questActivated) base.Play();
        }

        public override bool IsInteractable()
        {
            if (!_questActivated) return false;
            return base.IsInteractable();
        }

        private void Activate()
        {
            _questActivated = true;
        }
    }
}