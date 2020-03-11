using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    static ScenesManager instance;

    public static ScenesManager Instance() { return instance; }


    public enum Scene
    {
        INTRO = 0,
        MAIN = 1,
        OUTRO = 2
    }


    Scene currentScene = Scene.INTRO;

    public Scene CurrentScene { get => currentScene; set => currentScene = value; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveScene(Scene.MAIN);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetActiveScene(Scene scene)
    {
        currentScene = scene;

        CameraManager.Instance().SetCameraScene(scene);

        if(currentScene == Scene.MAIN)
        {
            FireManager.Instance().GetBurnables();
        }
    }


}
