using System.Collections;
using TMPro;
using UnityEngine;

namespace GUI.Inventory
{
    public class DisplayTextScreen : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        private bool _active;
        private bool _canBeClosed;

        public void Display(string displayedText)
        {
            Text.text = displayedText;
            gameObject.SetActive(true);
            _active = true;
            _canBeClosed = true;
        }
        
        public void Display(string displayedText, float time)
        {
            _canBeClosed = false;
            Text.text = displayedText;
            gameObject.SetActive(true);
            _active = true;
            StartCoroutine(WaitBeforeClosure(time));

        }

        private IEnumerator WaitBeforeClosure(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            _canBeClosed = true;
            
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

        public bool CanBeClosed()
        {
            return _canBeClosed;
        }
    }
}