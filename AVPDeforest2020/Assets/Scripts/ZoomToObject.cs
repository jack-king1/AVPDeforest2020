using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject earth;
    public float zoomSpeed = 10;
    [SerializeField] private float increaseSpeedTimer;

    private float speedIncrease = 1;
    void Start()
    {
        //earth = GameObject.FindGameObjectWithTag("AmazonRainforest");
    }

    // Update is called once per frame
    void Update()
    {
        float step = zoomSpeed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
        earth.transform.position, step);

        if(increaseSpeedTimer > 0)
        {
            increaseSpeedTimer -= Time.deltaTime;
        }
        else
        {
            zoomSpeed += speedIncrease;
            increaseSpeedTimer = 2.0f;
        }
    }
}
