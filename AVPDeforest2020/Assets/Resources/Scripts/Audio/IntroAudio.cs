using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class IntroAudio : MonoBehaviour
{
    public static IntroAudio instance;

    bool fadedIn;

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
        //AudioManagerOld.instance.FadeInSound("Idle", 5);
    }

    private void Update()
    {
        if (fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Intro, 3, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Intro"), transform, 1, 1, AudioManager.AudioChannel.Intro);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Intro, 10, false, GetComponentInChildren<AudioSource>());
    }
}
