using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;
using UnityEngine.SceneManagement;

public class ForestAudio : MonoBehaviour
{
    public static ForestAudio instance;

    bool fadedIn = false;
    bool stopHopeMusic = false;
    float timer = 20;

    public List<AudioClip> threeDSounds;
    public List<GameObject> threeDSoundsPositions;
    public List<GameObject> threeDGroundSoundsPositions;
    private AudioSource active3DSource;

    public float timer3d = 0;

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
        threeDSounds = new List<AudioClip>();
        Get3DSounds();
    }

    void Get3DSounds()
    {
        threeDSounds.Add(SFX.Instance.GetSFX("Birds"));
        threeDSounds.Add(SFX.Instance.GetSFX("Jaguar"));
        threeDSounds.Add(SFX.Instance.GetSFX("monkey1"));
        threeDSounds.Add(SFX.Instance.GetSFX("monkey2"));
        threeDSounds.Add(SFX.Instance.GetSFX("monkey3"));
        threeDSounds.Add(SFX.Instance.GetSFX("monkey4"));

    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            threeDSounds.Clear();
            threeDSoundsPositions.Clear();
            threeDGroundSoundsPositions.Clear();
            active3DSource = null;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Jungle, (1 - MainSceneManager.instance.burnPercent * 3) * 100f);
            if (active3DSource == null)
            {
                active3DSource = AudioManager.Instance.Play(threeDSounds[Random.Range(0, threeDSounds.Count)],
                    threeDSoundsPositions[Random.Range(0, threeDSoundsPositions.Count)].transform, 1, 1,
                    AudioManager.AudioChannel.Jungle);
                active3DSource.spatialBlend = 1;
                active3DSource.minDistance = 2;
                active3DSource.maxDistance = 10;
                timer3d = active3DSource.clip.length + 2f;
            }


            if (timer3d > 0)
            {
                timer3d -= Time.deltaTime;
            }
            else
            {
                active3DSource = AudioManager.Instance.Play(threeDSounds[Random.Range(0, threeDSounds.Count)],
                threeDSoundsPositions[Random.Range(0, threeDSoundsPositions.Count)].transform, 1, 1,
                AudioManager.AudioChannel.Jungle);
                timer3d = active3DSource.clip.length + 2f;
            }
        }

        if (fadedIn == false)
        {
            fadedIn = true;
            AudioManager.Instance.FadeMixer(AudioManager.AudioChannel.Jungle, 5, true, new AudioSource());
            AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Jungle"), transform, 1, 1, AudioManager.AudioChannel.Jungle);
            Narration.instance.StartCoroutine(Narration.instance.JungleNarration());
            //BirdSounds.instance.BirdSound(); -this wotn work :(   
        }

        if (stopHopeMusic)
        {
            if (timer > 0)
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
        JaguarSounds.instance.StopJaguarSound();
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