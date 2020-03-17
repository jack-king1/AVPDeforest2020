using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporate : MonoBehaviour
{
    bool evaporateStart = false;
    float timer = 5f;
    float speed = 5f;

    bool subscribed = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(!subscribed)
        {
            MainSceneManager.instance.fireStartedDelegate += setEvaporation;
            subscribed = true;
        }

        if(evaporateStart)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                transform.Translate((-Vector3.up) * Time.deltaTime);
            }
            else { evaporateStart = false; }

        }
    }

    void setEvaporation()
    {
        Debug.Log("Evaporation Started");
        evaporateStart = true;
    }
}
