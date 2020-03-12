using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    Text[] texts;
    public float fadeOutTime;
    public float timer = 8f;
    bool textFade = false;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(!textFade)
        {
            textFade = true;
            StartFade();
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

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
