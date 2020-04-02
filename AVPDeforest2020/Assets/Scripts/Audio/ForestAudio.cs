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

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);

   
    }

    private void Update()
    {
        if (fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 5, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Jungle"), transform, 1, 1, AudioManager.AudioChannel.Jungle);
            Narration.instance.StartCoroutine(Narration.instance.JungleNarration() );
            BirdSounds.instance.BirdSound();

           
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
        BirdSounds.instance.StopBird();
      //  Jaguar.instance.StopJaguarSound();
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