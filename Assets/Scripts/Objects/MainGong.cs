using Utility;

namespace Objects
{
    public class MainGong : Instrument
    {

        public Dialogue WrongTimeToPlayDialogue;
        
        /*
	    * PLAY
	    */

        public override void Play()
        {
            base.Play();
            if(DayProgressionManager.Instance.IsDayOver())
                DayProgressionManager.Instance.NextDay();
            else if (WrongTimeToPlayDialogue != null)
                WrongTimeToPlayDialogue.StartDialogue();
        }
    }
}