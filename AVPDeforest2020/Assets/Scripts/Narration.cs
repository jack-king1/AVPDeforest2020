using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    float timer = 0.0f;
    public int scenes = 5;
    private int Clips = 0;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Tropical");
        AudioManager.instance.Play("Jungle");
        AudioManager.instance.Play("Jungle1");
        AudioManager.instance.Play("Jungle2");
        AudioManager.instance.Play("Jungle3");
        AudioManager.instance.Play("Jungle4");
        AudioManager.instance.Play("Jungle5");
        AudioManager.instance.Play("Jungle6");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Narrations();
        Debug.Log(timer);
    }

    void Narrations()
    {
        #region OldCode
        //if( timer >= 14 && timer <= 15|| timer >= 29 && timer <= 30 || timer >= 44 && timer <= 45|| timer >= 59 && timer <= 60 || timer>= 74 && timer <= 75 || timer >= 89 &&  timer <= 90 )
        //{
        //    Debug.Log("Reseting playing bool");
        //    playing = false;
        //}

        //if (timer >= 0 && timer < 15 && timer <= 15 && !playing)
        //{
        //   Debug.Log("0 - 15");
        //   AudioManager.instance.Play("A");
        //   playing = true;
        //}

        //if (timer >= 15 && !playing)
        //{
        //    Debug.Log("15 - 29");
        //    AudioManager.instance.Play("B");
        //    playing = true;
        //}

        //if (timer >= 30 && !playing)
        //{
        //    Debug.Log("30 - 45");
        //    AudioManager.instance.Play("C");
        //    playing = true;
        //}


        //if (timer >= 45 && !playing)
        //{
        //    AudioManager.instance.Play("D");
        //    playing = true;
        //}

        //if (timer >= 60 && !playing)
        //{
        //    AudioManager.instance.Play("E");
        //    playing = true;
        //}

        //if (timer >= 75 && !playing)
        //{
        //    AudioManager.instance.Play("F");
        //    playing = true;
        //}
        //if (timer >= 90 && !playing)
        //{
        //    AudioManager.instance.Play("G");
        //    playing = true;
        //}

        #endregion 


        if( Clips == 0 && timer <= 4)
        {
            AudioManager.instance.Play("A");
            Clips++;
        }

        if(Clips == 1 && timer >= 5)
        {
            AudioManager.instance.Play("B");
            Clips++;
        }
        if (Clips == 2 && timer >= 18)
        {
            AudioManager.instance.Play("C");
            Clips++;
        }

        if (Clips == 3 && timer >= 26)
        {
            AudioManager.instance.Play("D");
            Clips++;
        }
        if (Clips == 4 && timer >= 40)
        {
            AudioManager.instance.Play("E");
            Clips++;
        }
        if (Clips == 5 && timer >= 55)
        {
            AudioManager.instance.Play("F");
            Clips++;
        }

        if (Clips == 6 && timer >= 65)
        {
            AudioManager.instance.Play("G");
            Clips++;
        }
        /* int clip 
             if (clip = 0 && timer <=4 ) 
             {
             play sound 
             clip ++; 
             }

        if ( clip == 1 && timer >= 5)
        {
            play sound 
            ++ clip
        }
         */
    }
}
