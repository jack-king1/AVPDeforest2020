using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    float timer = 0.0f;
    public int scenes;
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
        if(scenes == 2)
        {
           Scene2();
            
        }

        if(scenes == 3)
        {
            Scene3();
        }

        if (scenes == 4 )
        {

        }

        if (scenes == 5)
        {

        }
        
      
    }


    void Scene2()
    {

        if (Clips == 0 && timer <= 4)
        {
            AudioManager.instance.Play("A");
            Clips++;
        }

        if (Clips == 1 && timer >= 5)
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
            Clips = 0;
        }


    }

    void Scene3()
    {
        if (Clips == 0 && timer <= 4)
        {
            AudioManager.instance.Play("A");
            Clips++;
        }

        if (Clips == 1 && timer >= 5)
        {
            AudioManager.instance.Play("B");
            Clips++;
        }
        if (Clips == 2 && timer >= 18)
        {
            AudioManager.instance.Play("C");
            Clips++;
        }
    }

}
