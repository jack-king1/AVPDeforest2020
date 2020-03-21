using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{

   public bool startSFX = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Scene Trigger Hit");
            startSFX = true;
            AudioManagerOld.instance.Stop("Intro");

            AudioManagerOld.instance.Play("Jungle");
            AudioManagerOld.instance.Play("Cicada");
            AudioManagerOld.instance.Play("Alt");
          

        }
    }
}