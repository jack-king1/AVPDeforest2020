using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class ForestAudio : MonoBehaviour
{
    public static ForestAudio instance;
    
    bool fadedIn = false;
    bool stopHopeMusic = false;
    float timer = 20;

    //Monkey Vals
    //bool stopMonkeys = false;
    //private Transform monkeyTr;
    //private AudioSource Monkey;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //GameObject monkeyObject;
        //monkeyObject = GameObject.Find("Monkey");
        ////monkeyTr = monkeyObject.transform;
    }

    private void Update()
    {
        if (fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 5, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Jungle"), transform, 1, 1, AudioManager.AudioChannel.Jungle);
            Narration.instance.StartCoroutine(Narration.instance.JungleNarration() );
            

           
        }

        if(stopHopeMusic)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //if(stopMonkeys)
        //{
        //    Monkey.volume -= Time.deltaTime/50;
        //}
    }
    #region Scene Audio
    public void StopJungle()
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 4, false, new AudioSource());
    }

    public void StartHope()
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Hope, 5, true, new AudioSource());
        AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Hope"), transform, 1, 1, AudioManager.AudioChannel.Hope);
    }

    public void StopHope()
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Hope, 20, false, new AudioSource());
    }

    public void StopFire()
    {
        AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Fire, 10, false, new AudioSource());
    }

    public void BeginDestroyTimer()
    {
        stopHopeMusic = true;
    }

    #endregion 


}