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
       // StartCoroutine(PlayScene1());
    }

  public  IEnumerator PlayScene1()
    {
        //Scene 1
        yield return new WaitForSeconds(3);
        AudioManager.instance.Play("Clip1");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("Clip2");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("Clip3");
        yield return new WaitForSeconds(10);
        AudioManager.instance.Play("Clip4");
     
        //Scene 2
     
        //AudioManager.instance.Play("Clip5");
        //yield return new WaitForSeconds(10);
        //AudioManager.instance.Play("Clip6");
        //yield return new WaitForSeconds(10);
        //AudioManager.instance.Play("Clip7");

        //Scene 3 
        //AudioManager.instance.Play("Clip8");
        //yield return new WaitForSeconds(10);
        //AudioManager.instance.Play("Clip9");


        //AudioManager.instance.Play("G");
        //yield return new WaitForSeconds( AudioManager.instance.s.source.clip.length);

    }


}


