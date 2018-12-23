using System.Collections;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [Header("Alpha value")]
    [Range(0f, 1f)]
    public float Alpha = 1f;

    [Header("Time")]
    public float TransitionTime = 1f;
    
    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;
    
    private void Start()
    {
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("LayerSwitch"))
        {
            Player.Instance.ChangeLayer("CharacterBehind");
            ChangeAlpha(Alpha);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LayerSwitch"))
        {
            Player.Instance.ChangeLayer("CharacterFront");
            
            ChangeAlpha(1f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("LayerSwitch") && Player.Instance.GetLayer() == "CharacterFront")
        {
            Player.Instance.ChangeLayer("CharacterBehind");
            ChangeAlpha(Alpha);
        }
    }

    /// <summary>
    /// Starts the coroutine to change the alpha value of the sprite associated with this trigger
    /// </summary>
    /// <param name="endAlpha">Final value for the alpha value of the sprite</param>
    private void ChangeAlpha(float endAlpha)
    {
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(ChangeAlphaCoroutine(endAlpha));
    }
    
    /// <summary>
    /// Coroutine that changes the alpha value of the sprite
    /// </summary>
    /// <param name="endAlpha">Final value for the alpha value of the sprite</param>
    /// <returns></returns>
    private IEnumerator ChangeAlphaCoroutine(float endAlpha)
    {
        Color tmp = _spriteRenderer.color;

        for (float t = 0; t < 1.0f; t += Time.deltaTime / TransitionTime)
        {
            tmp.a = Mathf.Lerp(tmp.a, endAlpha, t);
            _spriteRenderer.color = tmp;

            yield return null;
        }
    }
    
}
