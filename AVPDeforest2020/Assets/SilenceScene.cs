using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceScene : MonoBehaviour
{
    public GameObject destroyedTerrain;

    private void Start()
    {
        MainSceneManager.instance.fireBurnProgressDelegate += AddDestroyedTerrain;
    }

    void AddDestroyedTerrain()
    {
        destroyedTerrain.SetActive(true);
    }
}
