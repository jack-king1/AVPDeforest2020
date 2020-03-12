using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject earth;
    public float zoomSpeed = 10;
    [SerializeField] private float increaseSpeedTimer;
    [SerializeField] private float resetTime;
    [SerializeField] private float maxSpeed;


    private float speedIncrease = 5;
    void Start()
    {
        //earth = GameObject.FindGameObjectWithTag("AmazonRainforest");
    }

    // Update is called once per frame
    void Update()
    {
        float step = zoomSpeed * Time.deltaTime;
        if(earth != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,earth.transform.position, step);
        }


        if(increaseSpeedTimer > 0)
        {
            increaseSpeedTimer -= Time.deltaTime;
        }
        else
        {
            if(zoomSpeed> maxSpeed)
            {
                zoomSpeed += speedIncrease;
                increaseSpeedTimer = resetTime;
            }
        }
    }
}
