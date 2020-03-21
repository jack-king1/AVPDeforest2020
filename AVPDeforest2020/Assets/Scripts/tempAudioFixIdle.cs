using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class tempAudioFixIdle : MonoBehaviour
{
    bool test = false;
    public AudioClip clip;

    private void Start()
    {
        //AudioClip clip = SFX.instance.GetSFX("Idle");
        //AudioManager.Instance.Play(clip, Camera.main.transform);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //AudioClip clip = SFX.Instance.GetSFX("Idle");
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Idle, 5f, true, new AudioSource());
            AudioManager.Instance.PlayLoop(clip, transform, 1, 1, AudioManager.AudioChannel.Idle);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //AudioClip clip = SFX.Instance.GetSFX("Idle");
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Idle, 5f, false, GetComponentsInChildren<AudioSource>());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(GetComponentInChildren<AudioSource>());
        }
    }
}
