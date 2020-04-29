using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class OneShotBirdSound : MonoBehaviour
{
    float timer = 0;
    AudioSource birdSound;
    float min = 1, max = 10;

    private void Awake()
    {
        timer = Random.Range(min, max);
    }

    public void Update()
    {
        AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Jungle, MainSceneManager.instance.burnPercent);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            birdSound = AudioManager.Instance.Play(SFX.Instance.GetSFX("Birds"), gameObject.transform, 1, 1, AudioManager.AudioChannel.Jungle);
            birdSound.spatialBlend = 1;
            birdSound.minDistance = 10;
            birdSound.maxDistance = 30;
            timer = Random.Range(birdSound.clip.length + min, birdSound.clip.length + max);
        }
    }
}
