﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
}
