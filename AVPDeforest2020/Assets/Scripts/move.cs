using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float Speed;

    public void Init(float speed)
    {
        GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.0f);
        Speed = speed;
    }

    public void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }
}
