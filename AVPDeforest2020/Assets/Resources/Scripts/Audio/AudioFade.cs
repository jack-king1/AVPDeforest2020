using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private AudioSource Fire;
     float fadeTime = 1;
    float startVolume = 0;

    public void Awake()
    {
        Fire = FireManager.Instance().FireSoundPrefab.GetComponent<AudioSource>();

       // startVolume = Fire.volume -= Fire.volume;
    }

    public IEnumerator FadeOut( float FadeTime)
    {
        float startVolume = Fire.volume;

        while (Fire.volume > 0)
        {
            Fire.volume -= startVolume * Time.deltaTime / FadeTime ;

            yield return null;
        }

     
       Fire.volume = startVolume;
    }


}
