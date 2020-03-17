using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float Speed;

    public void Init(float speed)
    {
        Speed = speed;
    }

    public void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }
}
