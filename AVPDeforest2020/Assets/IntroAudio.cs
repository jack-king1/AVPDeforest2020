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
        AudioManagerOld.instance.FadeInSound("Idle", 5);
    }

    public void StopSounds(float timeToFade)
    {
        AudioManagerOld.instance.FadeOutSound("Intro", timeToFade);
    }

    public void StartForestSounds(float timeToFade)
    {
        AudioManagerOld.instance.FadeInSound("Alt", timeToFade);
        AudioManagerOld.instance.FadeInSound("Cicada", timeToFade);
        AudioManagerOld.instance.FadeInSound("Jungle", timeToFade);
    }

    private void OnTriggerEnter(Collider other)
    {
        // SFX.instance.StartForestSounds(20);
        //SFX.instance.StopIntroSounds(5);
    }
}
