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
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
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

    }


}


