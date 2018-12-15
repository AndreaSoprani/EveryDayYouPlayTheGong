using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace GUI.Inventory
{
    public class ObjectiveNotificationController : MonoBehaviour
    {
        public TextMeshProUGUI NewObjectiveText;
        public Image Background;
        [Range(0f,10f)] public float OnScreenTime;

        private Coroutine _currentCoroutine;

        public void ShowNewObjective(string description)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                NewObjectiveText.enabled = false;
            }

            Background.enabled = true;
            NewObjectiveText.text = "You have a new objective: " + description;
            NewObjectiveText.enabled = true;
            
            
            _currentCoroutine = StartCoroutine(WaitAndClose());
        }

        private IEnumerator WaitAndClose()
        {
            yield return new WaitForSeconds(OnScreenTime);
            NewObjectiveText.enabled = false;
            Background.enabled = false;
        }
    }
}