using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterial : MonoBehaviour
{
    public MeshRenderer[] mats;
    public float fadeOutTime;
    public float timer = 3.5f;
    bool textFade = false;
    public Color c;

    void Start()
    {
        mats = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (!textFade)
        {
            textFade = true;
            StartFade();
        }
    }

    private void StartFade()
    {
        if( mats != null)
        {
            foreach (var m in mats)
            {
                m.material.color = c;
                StartCoroutine(FadeTextToFullAlpha(8, m));
            }
        }
        else
        {
            Debug.Log("No materials found");
        }

    }

    public IEnumerator FadeTextToFullAlpha(float t, MeshRenderer i)
    {
        i.material.color = new Color(i.material.color.r, i.material.color.g, i.material.color.b, 0);
        while (i.material.color.a < 1.0f)
        {
            i.material.color = new Color(i.material.color.r, i.material.color.g, i.material.color.b, i.material.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
