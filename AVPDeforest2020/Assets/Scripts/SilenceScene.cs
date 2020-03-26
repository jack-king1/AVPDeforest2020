using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceScene : MonoBehaviour
{
    public GameObject destroyedTerrain;
    bool delegateSet = false;

    private void Start()
    {
        StartCoroutine(DelaySetDelegate());
    }

    void AddDestroyedTerrain()
    {
        Debug.Log("Activating Destruction Scene");
        destroyedTerrain.SetActive(true);
    }

    IEnumerator DelaySetDelegate()
    {
        yield return new WaitForSeconds(2);
        MainSceneManager.instance.fireBurnProgressDelegate += AddDestroyedTerrain;
    }
}
