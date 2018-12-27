using System;
using System.Text;

namespace Objects
{
    public class Clock : InGameObject
    {
        public string DefaultString;
        public string AudioEvent;
        
        public override void Interact()
        {
            if (DefaultString != null)
            {
                StringBuilder output = new StringBuilder(DefaultString);
                output.Append(DateTime.Now.Hour);
                output.Append(":");
                output.Append(DateTime.Now.Minute);
                
                TextBoxManager.Instance.LoadString(output.ToString());
                TextBoxManager.Instance.EnableTextBox();
            }

            if (AudioEvent != null)
            {
                AudioManager.Instance.PlayEvent(AudioEvent);
            }
        }
    }
}