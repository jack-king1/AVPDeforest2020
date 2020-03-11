using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{

   public bool startSFX = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Scene Trigger Hit");
            SceneManagerKing.instance.FadeToLevel();
            startSFX = true;
            AudioManager.instance.Stop("Intro");

            AudioManager.instance.Play("Jungle");
            AudioManager.instance.Play("Cicada");
            AudioManager.instance.Play("Alt");


        }
    }
}