using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOut : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeOutStart;
    bool fadeBegin = false;
    IntroManager im;

    void Start()
    {
        text = GetComponent<Text>();
        im = GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutStart > 0)
        {
            fadeOutStart -= Time.deltaTime;
            if(fadeOutStart <= 0)
            {
                fadeOutStart = 0.0f;
                fadeBegin = true;
            }
        }
        else if(fadeBegin)
        {
            fadeTime -= Time.deltaTime;
            var tempColour = text.color;
            float fadeAlpha = Mathf.Lerp(0, 1, fadeTime);
            tempColour.a = fadeAlpha;
            text.color = tempColour;
            if(fadeTime <= 0)
            {
                fadeTime = 0;
                fadeBegin = false;  
            }
            else if(fadeTime <= 1.5f)
            {
                im.TextFadeComplete = true;
            }
        }
    }
}
