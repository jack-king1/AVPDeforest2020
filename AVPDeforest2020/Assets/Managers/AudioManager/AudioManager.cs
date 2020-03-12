using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/// <summary>
///  Singleton Class - Only one instance of this script will exist.
/// </summary>

//To access the audio manager in another script just type AudioManager.Play("Sound Name Here");
// To add more sounds go to Unity -> Select AudiManager Script which will be attached to GameManager, Increase Array Size by 1 and add drag sound into it.

public class AudioManager : MonoBehaviour
{
    public Sound[] JungleSounds;
    public Sound[] MiscSounds;
    public Sound[] Narration;
    public Sound[] Music;

    [SerializeField] private List<Sound> sounds;


    public static AudioManager instance;
  

    void Awake()
    {
        sounds = new List<Sound>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AddSoundsToList();
    }
    private void Start()
    {
        //Play("0");
        //Play("1");
        
       Play("Intro");
    }

    //Delete this update call, this is jsut to test stuff
    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("p Pressed");
            StartCoroutine(FadeIn("Intro", 10));
        }
    }

    public void Play(string sound_name)
    {
        Sound s = sounds.Find(sound => sound.name == sound_name);
        if (s == null)
        {
            Debug.Log("Sound with name: " + sound_name + " was not found.");
            return;
        }
        Debug.Log("Playing: " + s.source.name);
        s.source.Play();
    }

    public void Stop(string sound_name)
    {
        Sound s = sounds.Find(sound => sound.name == sound_name);
        if (s == null)
        {
            Debug.Log("Sound with name: " + sound_name + " was not found.");
            return;
        }
        s.source.Stop();
    }

    public IEnumerator FadeIn(string sound_name, float FadeTime)
    {
        Sound s = sounds.Find(sound => sound.name == sound_name);
        s.source.Play();
        s.source.volume = 0;

        while (s.source.volume < 1)
        {
            s.source.volume +=  Time.deltaTime / FadeTime;
            Debug.Log("volume:" + s.source.volume);
            yield return null;
        }
    }

    public IEnumerator FadeOut(string sound_name, float FadeTime)
    {
        Sound s = sounds.Find(sound => sound.name == sound_name);
        float startVolume = s.source.volume;

        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        Stop(sound_name);
        s.source.volume = startVolume;
    }

    void AddSoundsToList()
    {
        if (JungleSounds != null)
        {
            foreach (Sound s in JungleSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                sounds.Add(s);
            }
        }

        if (MiscSounds != null)
        {
            foreach (Sound s in MiscSounds)
            {
                Debug.Log("Adding sound: " + s.name);
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                sounds.Add(s);
            }
        }

        if (Narration != null)
        {
            foreach (Sound s in Narration)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                sounds.Add(s);
            }
        }

        if (Music != null)
        {
            foreach (Sound s in Music)
            {
                Debug.Log("Adding sound: " + s.name);
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                sounds.Add(s);
            }
        }

        //Delete other arrays here so they arnt held in memory.
    }


 

    
}
