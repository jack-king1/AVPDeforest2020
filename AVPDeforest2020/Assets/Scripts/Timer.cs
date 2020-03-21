using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float maxTime;
    public float minTime;
    public float timer;


    public void Init(float _minTime, float _maxTime)
    {
        minTime = _minTime;
        maxTime = _maxTime;
        SetTimer();
    }

    public void Init(float _time)
    {
        minTime = _time;
        maxTime = _time;
        SetTimer();
    }

    public void SetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }

    ////Returns tree when timer <= 0 and resets
    public bool Count()
    {
        if(timer <= 0)
        {
            SetTimer();
            return true;

        }
        else
        {
            timer -= Time.deltaTime;
            return false;
        }
    }
}
