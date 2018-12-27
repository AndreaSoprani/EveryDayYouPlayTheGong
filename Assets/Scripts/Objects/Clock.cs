using System;
using System.Text;

namespace Objects
{
    public class Clock : InGameObject
    {
        public string DefaultString;
        public AreaInstrument Instrument;
        
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
            Instrument.Interact();

           
        }
    }
}