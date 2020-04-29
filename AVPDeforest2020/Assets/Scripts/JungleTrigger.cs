using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleTrigger : MonoBehaviour
{
    public bool startSFX = false;
    Narration narration;


    private void Awake()
    {
        narration = GetComponent<Narration>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            startSFX = true;
            //AudioManager.instance.Stop("Intro");
            //AudioManager.instance.Play("Jungle");
            //AudioManager.instance.Play("Cicada");
            //AudioManager.instance.Play("Alt");
            //narration.StartCoroutine(narration. PlayScene1());
            //Debug.Log("Im Playin");

        }
    }
}

