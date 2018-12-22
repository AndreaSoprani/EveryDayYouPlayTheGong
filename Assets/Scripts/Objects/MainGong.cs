using System.Collections;
using UnityEngine;
using Utility;

namespace Objects
{
    public class MainGong : Instrument
    {

        public Dialogue WrongTimeToPlayDialogue;
        public TextAsset NewDayText;
        
        /*
	    * PLAY
	    */

        public override void Play()
        {
            base.Play();
            if (DayProgressionManager.Instance.IsDayOver())
            {
                DayProgressionManager.Instance.NextDay();
                if(NewDayText != null) StartCoroutine(DisplayText());
            }
            else if (WrongTimeToPlayDialogue != null)
                WrongTimeToPlayDialogue.StartDialogue();
        }

        private IEnumerator DisplayText()
        {
            yield return new WaitForSeconds(2);
            GameObject.FindGameObjectWithTag("GUIController").SendMessage("DisplayText", NewDayText.text);
        }
    }
}