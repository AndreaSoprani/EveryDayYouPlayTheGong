using TMPro;
using UnityEngine;

namespace GUI.Inventory
{
    public class DisplayTextScreen : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        private bool _active;

        public void Display(string displayedText)
        {
            Text.text = displayedText;
            gameObject.SetActive(true);
            _active = true;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _active = false;
            Text.text = "";
        }

        public bool IsActive()
        {
            return _active;
        }
        
    }
}