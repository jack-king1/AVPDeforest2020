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

            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Hope, 5f, true);
            AudioManager.Instance.Play(SFX.Instance.GetSFX("Hope"), CameraManager.instance.transform);
        }
    }
}
