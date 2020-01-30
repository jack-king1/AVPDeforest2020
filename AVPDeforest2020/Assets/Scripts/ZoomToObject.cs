using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject earth;
    [SerializeField] private IntroManager im;
    [SerializeField] private float zoomSpeed = 10;
    void Start()
    {
        earth = GameObject.FindGameObjectWithTag("AmazonRainforest");
        im = GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(im.TextFadeComplete)
        {
            float step = zoomSpeed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, earth.transform.position, step);
        }
    }
}
