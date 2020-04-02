using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSounds : MonoBehaviour
{
    public static BirdSounds instance;

    bool stopBird = false;

    private AudioSource bird;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (stopBird)
        {
            bird.volume -= Time.deltaTime / 50;
        }


    }
    #region BirdSounds
    public void BirdSound()
    {
        bird = AudioManagerNS.AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Birds"), transform, 1.5f, 1, AudioManagerNS.AudioManager.AudioChannel.Jungle);
        bird.spatialBlend = 1f;
        bird.minDistance = 1000;
        bird.maxDistance = 2000;
    }

    public void StopBird()
    {
        stopBird = true;
    }

    #endregion
}
