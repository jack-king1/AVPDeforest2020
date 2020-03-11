using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    bool usingVr = false;

    public GameObject VrRaycast;
    public GameObject PcRaycast;

    public enum SceneStage
    {
        TRANQUIL = 0,
        BURNING = 1,
        HOPE = 2
    }

    SceneStage currentStage = SceneStage.TRANQUIL;
    float sceneStageTime = 60.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScenesManager.Instance().CurrentScene == ScenesManager.Scene.MAIN)
        {
            if (sceneStageTime <= 0.0f)
            {
                switch (currentStage)
                {
                    case SceneStage.TRANQUIL:
                        {
                            currentStage = SceneStage.BURNING;
                            sceneStageTime = 90.0f;

                            if(usingVr)
                            {
                                VrRaycast.GetComponent<CameraRaycast>().enabled = true;
                            }
                            else
                            {
                                PcRaycast.GetComponent<CameraRaycast>().enabled = true;
                            }
                            break;
                        }
                    case SceneStage.BURNING:
                        {
                            currentStage = SceneStage.HOPE;
                            sceneStageTime = 30.0f;

                            if (usingVr)
                            {
                                VrRaycast.GetComponent<CameraRaycast>().enabled = false;
                            }
                            else
                            {
                                PcRaycast.GetComponent<CameraRaycast>().enabled = false;
                            }
                            break;
                        }
                    case SceneStage.HOPE:
                        {
                            ScenesManager.Instance().SetActiveScene(ScenesManager.Scene.OUTRO);
                            break;
                        }
                }
            }
            sceneStageTime -= Time.deltaTime;
        }
    }
}
