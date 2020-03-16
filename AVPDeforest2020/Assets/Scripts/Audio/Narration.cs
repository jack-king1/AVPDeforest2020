using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narration : MonoBehaviour
{
    public static Narration instance;
    AudioSource m_MyAudioSource;

    bool a = false;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(this);
        }

        GetComponent<AudioSource>().loop = true;
       // StartCoroutine(PlayScene1());
    }

    public IEnumerator PlayScene1()
    {
        //Scene 1
        if (!a)
        {
            yield return new WaitForSeconds(3);
            AudioManagerOld.instance.Play("Clip1");
            yield return new WaitForSeconds(10);
            AudioManagerOld.instance.Play("Clip2");
            yield return new WaitForSeconds(10);
            AudioManagerOld.instance.Play("Clip3");
            yield return new WaitForSeconds(10);
            AudioManagerOld.instance.Play("Clip4");
            a = true;
        }


    }
        //Scene 2

    public IEnumerator PlayScene2()
    {
        //AudioManager.instance.Play("Clip5");
        yield return new WaitForSeconds(10);
        //AudioManager.instance.Play("Clip6");
        //yield return new WaitForSeconds(10);
        //AudioManager.instance.Play("Clip7");
    }
     
     
    public IEnumerator PlayScene3()
    {

        AudioManagerOld.instance.Play("Clip8");
        yield return new WaitForSeconds(10);
        AudioManagerOld.instance.Play("Clip9");


        AudioManagerOld.instance.Play("G");
        yield return new WaitForSeconds(10);
    }
        

    


}


