using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateDemo : MonoBehaviour
{ 
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 2, 0, Space.World);
    }
}
