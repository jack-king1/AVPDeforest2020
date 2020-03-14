using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRFadeCustom : MonoBehaviour
{
    public bool fadeIn = false;
    public GameObject VRCam;
    public float fadeTime;
    bool newScene = false;
    public float newSceneTimer = 5;
    public float timer;

    public OVRScreenFade ovrscreenfade;

    public SceneType fadeToScene;

    private void Update()
    {
        if(newScene)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                newScene = false;
               ScenesManager.Instance().LoadScene(fadeToScene);
            }
        }
        else
        {
            timer = newSceneTimer;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
            newScene = true;
            timer = newSceneTimer;
        }
    }

    public void FadeOut()
    {
        Debug.Log("Fadeing Out!");
        ovrscreenfade = VRCam.GetComponentInChildren<OVRScreenFade>();
        ovrscreenfade.FadeOut(fadeTime);
    }

    public void FadeIn()
    {
        Debug.Log("Fadeing In!");
        ovrscreenfade = VRCam.GetComponentInChildren<OVRScreenFade>();
        ovrscreenfade.FadeIn(fadeTime);
    }
}
