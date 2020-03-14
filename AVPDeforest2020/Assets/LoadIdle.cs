using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Camera.main.GetComponent<OVRScreenFade>().FadeOut(5f, SceneType.IDLE);
                loadingScene = true;
            }
        }
    }
}
