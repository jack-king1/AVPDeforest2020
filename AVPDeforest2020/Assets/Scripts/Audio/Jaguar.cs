using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaguarSounds : MonoBehaviour
{
    public static JaguarSounds instance;

    bool stopJaguar = false;

    private AudioSource jaguar;

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
        if (stopJaguar)
        {
            jaguar.volume -= Time.deltaTime / 50;
        }


    }
    #region MonkeySounds
    public void JaguarSound()
    {
        jaguar = AudioManagerNS.AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Jaguar"), transform, .35f, 1, AudioManagerNS.AudioManager.AudioChannel.Jungle);
        jaguar.spatialBlend = 1f;
        jaguar.minDistance = 50;
        jaguar.maxDistance = 2000;
    }

    public void StopJaguarSound()
    {
        stopJaguar = true;
    }

    #endregion
}
