using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;

    public static IEnumerator FadeAlpha(Action<Color> objectColor, float fadeTime, 
        float currentAlpha, float newAlpha)
    {
        float time = 0;
        float timeLimit = fadeTime;
        while (time < timeLimit)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(currentAlpha,newAlpha, time));
            objectColor(newColor);
            time += Time.deltaTime;
            yield return null;
        }
    }

    /* **WIP** */
    //public static IEnumerator FadeColour(Action<Color> objectColor, float fadeTime,
    //Color currentColor, Color newColor)
    //{
    //    float time = 0;
    //    float timeLimit = fadeTime;
    //    while (time < timeLimit)
    //    {
    //        Color newColor = new Color(1, 1, 1, Mathf.Lerp(currentAlpha, newAlpha, time));
    //        objectColor(newColor);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
