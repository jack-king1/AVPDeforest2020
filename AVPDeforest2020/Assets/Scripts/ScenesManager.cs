using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{



    static ScenesManager instance;


    public static ScenesManager Instance() { return instance; }

    public GameObject[] scenes = new GameObject[3];

    public enum Scene
    {
        INTRO = 0,
        MAIN = 1,
        OUTRO = 2
    }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveScene(Scene.INTRO);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetActiveScene(Scene scene)
    {
        for(int i = 0; i < scenes.Length; ++i)
        {
            if (i == (int)scene)
            {
                scenes[i].SetActive(true);
                CameraManager.Instance().SetCameraScene(scene);
            }
            else
                scenes[i].SetActive(false);
        }

    }


}
