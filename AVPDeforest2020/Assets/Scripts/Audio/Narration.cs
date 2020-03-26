using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;
[RequireComponent(typeof(AudioSource))]
public class Narration : MonoBehaviour
{
    public static Narration instance;
    AudioSource m_MyAudioSource;

    bool a = false;
    int timebuffer = 5;
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

    public IEnumerator JungleNarration()
    {
        //Scene 1
        if (!a)
        {
            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration1"), transform);

            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration1").length +timebuffer);

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration2"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration2").length + timebuffer);

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration3"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration3").length + timebuffer);

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration4"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration4").length + timebuffer);

         
            a = true;
        }


    }
        //Scene 2

    public IEnumerator FireNarration()
    {
        AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration5"), transform);
        yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration5").length + timebuffer);

        AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration6"), transform);
        yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration6").length + timebuffer);

    }
     
     
    public IEnumerator HopeNarration()
    {
        if (a)
        {
            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration7"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration7").length + timebuffer);

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration8"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration8").length + timebuffer);

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration9"), transform);
            yield return new WaitForSeconds(SFX.Instance.GetSFX("Narration9").length + timebuffer);

            a = false;
        }

       
    }
        

    


}


