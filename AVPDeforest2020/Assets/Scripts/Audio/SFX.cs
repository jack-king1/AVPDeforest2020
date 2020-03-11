using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private int audioSeed;
    public float timer;
    private AudioSource source;
    NextScene scene;

    public static SFX instance;

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }


        scene = gameObject.GetComponent<NextScene>();
        //AudioManager.instance.Play("Jungle1");
        //AudioManager.instance.Play("Cicada");
        //AudioManager.instance.Play("Alt");

    }
    private void Update()
    {
        if(scene.startSFX)
        {
            timer += Time.deltaTime;
        }
       
        Debug.Log(timer);
        if (timer >= 60 && timer <= 61 )   JungleSounds();

        if (timer >= 110 && timer <= 111) WindSounds();
    }

    
    public void JungleSounds()
    {
        AudioManager.instance.Stop("Alt");
        AudioManager.instance.Stop("Jungle");
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

    void WindSounds()
    {
        AudioManager.instance.Play("Whoosh");
    }


    void ResetValues()
    {
        timer = 0;
    }

    
}
