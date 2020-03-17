using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToQuote : MonoBehaviour
{
    public GameObject VRCam;
    public GameObject quote;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FadeToQuote");
        FadeOut();
        quote.SetActive(true);
    }

    public void FadeOut()
    {
        Debug.Log("Fadeing Out!");
        VRCam.GetComponent<OVRScreenFade>().FadeOut(3.5f);
    }

    public void FadeIn()
    {
        Debug.Log("Fadeing In!");
        VRCam.GetComponent<OVRScreenFade>().FadeIn(3.5f);
    }
}
