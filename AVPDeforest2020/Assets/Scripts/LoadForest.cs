using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadForest : MonoBehaviour
{
    public float fadeTime;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponentInChildren<OVRScreenFade>().FadeOut(fadeTime, SceneType.MAIN);
    }
}
