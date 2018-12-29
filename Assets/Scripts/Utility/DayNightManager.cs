using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour {

    public SpriteRenderer SpAfternoon;
    public SpriteRenderer SpNight;

    public int TimeSlot = 3;
    [Range(0,10)] public float TransitionTime = 2f;

    public void ChangeTimeSlot()
    {
        TimeSlot++;
        switch (TimeSlot)
        {
            case 1:
                StartCoroutine(FadeMaskGradually(SpNight));
                break;
            case 2:
                StartCoroutine(ChangeMaskGradually(SpAfternoon));
                break;
            case 3:
                StartCoroutine(FadeMaskGradually(SpAfternoon));
                StartCoroutine(ChangeMaskGradually(SpNight));
                break;
            case 4:
                TimeSlot = 0;
                break;
        }
    }

    IEnumerator ChangeMaskGradually(SpriteRenderer Mask)
    {
        float timeElapsed = 0f;

        Color32 startColor = Mask.color;
        Color32 endColor = Mask.color;
        endColor.a = 255;

        while (timeElapsed < TransitionTime)
        {
            timeElapsed += Time.deltaTime;
            Mask.color = Color.Lerp(startColor, endColor, timeElapsed / TransitionTime);
            yield return null;
        }
    }

    IEnumerator FadeMaskGradually(SpriteRenderer Mask)
    {
        float timeElapsed = 0f;

        Color32 startColor = Mask.color;
        Color32 endColor = Mask.color;
        endColor.a = 0;

        while (timeElapsed < TransitionTime)
        {
            timeElapsed += Time.deltaTime;
            Mask.color = Color.Lerp(startColor, endColor, timeElapsed / TransitionTime);
            yield return null;
        }
    }
}
