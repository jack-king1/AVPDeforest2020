using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAudio : MonoBehaviour
{
    public static IdleAudio instance;

    private void Start()
    {
        AudioManager.instance.FadeInSound("Idle", 5);
    }

    public void StopSounds(float timeToFade)
    {
        AudioManager.instance.FadeOutSound("idle", timeToFade);
    }
}
