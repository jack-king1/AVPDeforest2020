using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;
using UnityEngine.SceneManagement;

public class LoadIdle : MonoBehaviour
{
    public float loadTimer;
    bool start = false;

    bool loadingScene = false;

    private void Start()
    {
        loadTimer = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            loadTimer = 10;
            start = true;
            if(ForestAudio.instance)
            {
                ForestAudio.instance.StopHope();
                ForestAudio.instance.BeginDestroyTimer();
            }
            else
            {
                Debug.LogWarning("No Forest Audio Instance - Probably ran without running forest scene.");
            }

            AudioManager.Instance.Play(SFX.Instance.GetSFX("Narration9"),Camera.main.transform, 1,1,AudioManager.AudioChannel.Narration);
        }
    }

    private void Update()
    {
        if(start && loadTimer > 0)
        {
            loadTimer -= Time.deltaTime;

        }
        else if(loadTimer <= 0)
        {
            if(!loadingScene)
            {
                start = false;
                //SFX.instance.StopOutroSounds(5);
                //SFX.instance.StartIdleSounds(10);
                SceneManager.LoadScene((int)SceneType.IDLE);
                loadingScene = true;
            }
        }
    }
}
