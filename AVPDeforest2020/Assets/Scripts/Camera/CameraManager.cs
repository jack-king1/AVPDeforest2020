using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera VRcam;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (VRcam == null)
        {
            VRcam = Camera.main;
        }
    }

    public void NewScene(SceneType sceneType, bool fadeOut)
    {
        if(fadeOut)
        {
            if (VRcam == null)
            {
                VRcam = Camera.main;
            }
            VRcam.GetComponent<OVRScreenFade>().FadeIn(5, sceneType);
        }
        else
        {
            ScenesManager.Instance.LoadScene(sceneType);
        }
    }
}
