using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    public float x_speed;
    public float y_speed;
    public float z_speed;

    private void Update()
    {
        transform.Rotate(Time.deltaTime * x_speed, Time.deltaTime * y_speed, Time.deltaTime * z_speed);
    }
}
