using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour
{
    public float startTime;

    float transitionTimer = 2.0f;
    public Image FadePanel;

    private void Start()
    {
        FadePanel = GetComponent<Image>();
    }

    void Update()
    {
        if(startTime <= 0)
        {
            transitionTimer -= Time.deltaTime;
            var tempColour = FadePanel.color;
            float fadeAlpha = Mathf.Lerp(1, 0, transitionTimer);
            tempColour.a = fadeAlpha;
            FadePanel.color = tempColour;
        }
        else
        {
            startTime -= Time.deltaTime;
            if (startTime < 0)
            {
                startTime = 0;
            }
        }
    }
}
