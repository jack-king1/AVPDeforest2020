using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class ForestAudio : MonoBehaviour
{
    public static ForestAudio instance;

    bool fadedIn = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 10, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Jungle"), transform, 1, 1, AudioManager.AudioChannel.Jungle);
        }
    }

    public void StopJungle()
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 10, false, new AudioSource());
    }

    public void StartWind()
    {
        //AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Fire, 10, true, new AudioSource());
        //AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Wind"), transform, 1, 1, AudioManager.AudioChannel.Fire);
    }
}