﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    static DontDestroyOnLoadScript instance;
    private void Awake()
    {
        if(DontDestroyOnLoadFlag.onDestroyCreated == false)
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (instance != null)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoadFlag.onDestroyCreated = true;
        }
    }
}
