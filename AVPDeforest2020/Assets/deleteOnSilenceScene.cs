using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteOnSilenceScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelaySetDelegate());
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    IEnumerator DelaySetDelegate()
    {
        yield return new WaitForSeconds(2);
        MainSceneManager.instance.fireBurnProgressDelegate += DestroySelf;
    }
}
