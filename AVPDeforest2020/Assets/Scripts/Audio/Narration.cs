using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narration : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        AudioManager.instance.Play("A");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("B");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("C");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("D");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("E");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("F");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("G");



        //AudioManager.instance.Play("G");
        //yield return new WaitForSeconds( AudioManager.instance.s.source.clip.length);
      
    }


}


