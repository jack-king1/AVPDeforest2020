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
        AudioManager.instance.FadeIn("Jungle1", 1);
        AudioManager.instance.Play("Cicada");
        AudioManager.instance.FadeIn("Cicada", 1);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        JungleSounds();
    }

    
    void JungleSounds()
    {
        if(timer >= 10 && timer <= 11)
        {
            AudioManager.instance.Play("Regular");
            AudioManager.instance.FadeIn("Regular", 1);
        }


        if (timer >= 30 && timer <= 31)
        {
            AudioManager.instance.FadeOut("Regular", 0);
            AudioManager.instance.Play("Alt");
            AudioManager.instance.FadeIn("Alt", 1);
        }

        if (timer >= 50 && timer <= 51)
        {
          AudioManager.instance.FadeOut("Alt", 0);
           AudioManager.instance.FadeOut("Jungle1", 0);
        }


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
        audioSeed = 0;
    }

    
}
