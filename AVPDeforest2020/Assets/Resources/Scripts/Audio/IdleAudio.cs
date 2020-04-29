using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class IdleAudio : MonoBehaviour
{
    public static IdleAudio instance;

    bool fadedIn = false;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Idle, 3, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Idle"), transform,1,1, AudioManager.AudioChannel.Idle);
        }
    }
}
