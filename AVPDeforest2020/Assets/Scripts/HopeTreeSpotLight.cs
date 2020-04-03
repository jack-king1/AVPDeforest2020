using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeTreeSpotLight : MonoBehaviour
{

    Light light;

    float time = 0.0f;
    float timeLimit = 10.0f;
    float maxIntensity = 30.0f;
    float minIntensity = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time < timeLimit)
        {
            light.intensity = Mathf.Lerp(minIntensity, maxIntensity, time / timeLimit);
            time += Time.deltaTime;
        }

    }
}
