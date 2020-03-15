using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    Text[] texts;

    public float fadeInAfterTime = 8f; // How long untill fade in begins (used for outro cause im a lazy b***)
    public float displayTextDuration; // how long the text is displayed

    public float fadeOutTime;// How long it takes to fade in/out
    public bool needsFadeOut;

    bool textFade = false;

    private void Update()
    {
        if (fadeInAfterTime > 0)
        {
            fadeInAfterTime -= Time.deltaTime;
        }
        else if (!textFade)
        {
            textFade = true;
            StartFade();
        }

        if(textFade && needsFadeOut)
        {
            if(displayTextDuration > 0)
            {
                displayTextDuration -= Time.deltaTime;
            }
            else
            {
                needsFadeOut = false;
                EndFade();
            }
        }

    }

    private void StartFade()
    {
        texts = GetComponentsInChildren<Text>();
        foreach(var t in texts)
        {
            StartCoroutine(FadeTextToFullAlpha(fadeOutTime, t));
        }
    }

    private void EndFade()
    {
        texts = GetComponentsInChildren<Text>();
        foreach (var t in texts)
        {
            StartCoroutine(FadeTextToZeroAlpha(fadeOutTime, t));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = i.color;
        while (i.color.a > 0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
