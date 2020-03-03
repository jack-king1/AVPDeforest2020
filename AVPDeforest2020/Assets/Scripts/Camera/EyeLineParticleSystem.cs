﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLineParticleSystem : MonoBehaviour
{

    ParticleSystem ps;

    float sizeLower = 1.0f;
    float sizeUpper = 1.0f;

    float rateUpper = 50.0f;
    float rateLower = 5.0f;

    float radialUpper = -2.0f;
    float radialLower = -0.5f;

    float radiusLower = 1.0f;
    float radiusUpper = 5.0f;

    float maxDistance = 50.0f;
    float minDIstance = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (gameObject.transform.position - CameraMovement.Instance().gameObject.transform.position).magnitude;

        var radiusSize = Mathf.Lerp(radiusLower, radiusUpper, distance/maxDistance);

        if (radiusSize > radiusUpper) radiusSize = radiusUpper;

        if (radiusSize < radiusLower) radiusSize = radiusLower;

        var lookTime = CameraMovement.Instance().LookTime;
        var maxLookTime = CameraMovement.Instance().MaxLookTime;

        var main = ps.main;
        var vel = ps.velocityOverLifetime;
        var emission = ps.emission;
        var shape = ps.shape;

        main.startSize = new ParticleSystem.MinMaxCurve(Mathf.Lerp(sizeLower, sizeUpper * (radiusSize > 3.0f ? radiusSize - 2.0f : radiusSize), lookTime / (maxLookTime * 0.8f)));
        vel.radial = new ParticleSystem.MinMaxCurve(Mathf.Lerp(radialLower, radialUpper * (radiusSize > 2.0f ? radiusSize - 1.0f : radiusSize), lookTime / (maxLookTime * 0.8f)));
        emission.rate = new ParticleSystem.MinMaxCurve(Mathf.Lerp(rateLower, rateUpper, lookTime / (maxLookTime * 0.8f)));
        shape.radius = radiusSize;



    }
}
