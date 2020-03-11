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
                ScenesManager.Instance().LoadNextScene();
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
        VRCam.GetComponent<OVRScreenFade>().FadeOut(fadeTime);
    }

    public void FadeIn()
    {
        Debug.Log("Fadeing In!");
        VRCam.GetComponent<OVRScreenFade>().FadeIn(fadeTime);
    }
}
