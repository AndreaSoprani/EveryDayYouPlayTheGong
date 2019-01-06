using System.Collections;
using UnityEngine;

namespace Objects
{
    public class AreaSound : MonoBehaviour
    {
        public string SoundEvent;
        [Range(0,5)] public float Delay;
        public bool DestroyAfterFirstTime;

        private Collider2D _collider;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(PlaySound());
            }
        }

        private IEnumerator PlaySound()
        {
            yield return new WaitForSecondsRealtime(Delay);
            AudioManager.Instance.PlayEvent(SoundEvent);
            if(DestroyAfterFirstTime) Destroy(gameObject);
        }
    }
}