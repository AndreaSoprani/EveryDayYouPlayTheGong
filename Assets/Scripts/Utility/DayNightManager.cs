using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour {

    public SpriteRenderer SpAfternoon;
    public SpriteRenderer SpNight;

    int TimeSlot = 3;

    public void ChangeTimeSlot()
    {
        TimeSlot++;
        switch (TimeSlot)
        {
            case 1:
                StartCoroutine(FadeMaskGradually(SpNight));
                break;
            case 2:
                break;
            case 3:
                StartCoroutine(ChangeMaskGradually(SpAfternoon));
                break;
            case 4:
                StartCoroutine(FadeMaskGradually(SpAfternoon));
                StartCoroutine(ChangeMaskGradually(SpNight));
                TimeSlot = 0;
                break;
        }
    }

    IEnumerator ChangeMaskGradually(SpriteRenderer Mask)
    {
        float timeElapsed = 0f;
        float transitionTime = 2f;

        Color32 startColor = Mask.color;
        Color32 endColor = Mask.color;
        endColor.a = 255;

        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            Mask.color = Color.Lerp(startColor, endColor, timeElapsed / transitionTime);
            yield return null;
        }
    }

    IEnumerator FadeMaskGradually(SpriteRenderer Mask)
    {
        float timeElapsed = 0f;
        float transitionTime = 2f;

        Color32 startColor = Mask.color;
        Color32 endColor = Mask.color;
        endColor.a = 0;

        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            Mask.color = Color.Lerp(startColor, endColor, timeElapsed / transitionTime);
            yield return null;
        }
    }
}
