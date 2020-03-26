using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateSmoke : MonoBehaviour
{ 
    private void Start()
    {
        MainSceneManager.instance.silenceSceneStartedDelegate += StartSmoke;
    }

    void StartSmoke()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
