using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private int audioSeed;
    private float timer;
    private AudioSource source;

    private void Start()
    {
        AudioManager.instance.Play("Jungle1");
        AudioManager.instance.Play("Cicada");
        AudioManager.instance.Play("Alt");

    }
    private void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 60 && timer <= 61 )   JungleSounds();
    }

    
    void JungleSounds()
    {

        AudioManager.instance.Stop("Alt");
        AudioManager.instance.Stop("Jungle1");
        AudioManager.instance.Stop("Cicada");


        #region OldCode 
        //if (audioSeed == 1 )
        //{

        //  AudioManager.instance.Play("Jungle1");
        //  ResetValues();

        //}

        //if (audioSeed == 2)
        //{

        //    AudioManager.instance.Play("Regular");
        //    ResetValues();
        //}
        //if (audioSeed == 3)
        //{

        //    AudioManager.instance.Play("Tropical");
        //    ResetValues();
        //}

        #endregion
    }

    void ResetValues()
    {
        timer = 0;
    }

    
}
