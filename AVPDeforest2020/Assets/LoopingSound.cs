using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;


public class LoopingSound : MonoBehaviour
{ 
    void Start()
    {
        AudioSource temp;
        temp = AudioManager.Instance.Play(SFX.Instance.GetSFX("Monkey"),gameObject.transform,1,1,AudioManager.AudioChannel.Jungle);
        temp.spatialBlend = 1;
    }
}
