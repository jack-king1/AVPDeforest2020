using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playJungleSounds : MonoBehaviour
{
    [SerializeField] private float timer = 10;

    private void Update()
    {
        if(timer <= 0)
        {
            int sound = Random.Range(1, AudioManagerOld.instance.JungleSounds.Length + 1);
            AudioManagerOld.instance.Play(sound.ToString());
            Debug.Log("Sound Played: " + sound);
            timer = Random.Range(10, 20);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
