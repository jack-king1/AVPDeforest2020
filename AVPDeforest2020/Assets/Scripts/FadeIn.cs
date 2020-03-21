using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float fadeTime;

    void Start()
    {
        text = GetComponent<Text>();
        var tempColor = text.color;
        tempColor.a = 0.0f;
        text.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            if(fadeTime <= 0)
            {
                fadeTime = 0;
                Destroy(this);
            }
        }
        var tempColour = text.color;
        float fadeAlpha = Mathf.Lerp(1, 0, fadeTime);
        tempColour.a = fadeAlpha;
        text.color = tempColour;
    }
}
