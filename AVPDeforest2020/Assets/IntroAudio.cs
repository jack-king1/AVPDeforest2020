using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudio : MonoBehaviour
{
    public static IntroAudio instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(this);
        }
        AudioManager.instance.FadeInSound("Idle", 5);
    }

    public void StopSounds(float timeToFade)
    {
        AudioManager.instance.FadeOutSound("Intro", timeToFade);
    }

    public void StartForestSounds(float timeToFade)
    {
        AudioManager.instance.FadeInSound("Alt", timeToFade);
        AudioManager.instance.FadeInSound("Cicada", timeToFade);
        AudioManager.instance.FadeInSound("Jungle", timeToFade);
    }

    private void OnTriggerEnter(Collider other)
    {
        SFX.instance.StartForestSounds(20);
        SFX.instance.StopIntroSounds(5);
    }
}
