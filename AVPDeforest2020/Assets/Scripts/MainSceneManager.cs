﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    [SerializeField] bool usingVr = false;
    public GameObject CameraGO;
    public GameObject hopeTreePrefab;

    GameObject hopeTreeSpawn;
    GameObject dirLight;
    Color startColour = new Color();

    bool nextSceneLoading = false;

    public enum SceneStage
    {
        TRANQUIL = 0,
        BURNING = 1,
        HOPE = 2
    }

    SceneStage currentStage = SceneStage.TRANQUIL;

    [SerializeField]float[] sceneStageTimes = new float[3];
    float sceneStageTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }

        sceneStageTime = sceneStageTimes[0];
        hopeTreeSpawn = GameObject.FindGameObjectWithTag("HopeTreeSpawn");
        dirLight = GameObject.FindGameObjectWithTag("DirectinalLight");

        Camera.main.GetComponent<CameraRaycast>().enabled = false;
        if(!usingVr) Camera.main.GetComponent<CameraMovement>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStageTime <= 0.0f)
        {
            switch (currentStage)
            {
                case SceneStage.TRANQUIL:
                    {
                        currentStage = SceneStage.BURNING;
                        sceneStageTime = sceneStageTimes[1];

                        StartCoroutine(ChangeSkyBox(5.0f));
                        StartCoroutine(ChangeDirectionalLight(2.0f));
                        Camera.main.GetComponent<CameraRaycast>().enabled = true;
                        //SFX.instance.JungleSounds();
                        StartCoroutine(Narration.instance. PlayScene2());
                        break;
                    }
                case SceneStage.BURNING:
                    {
                        currentStage = SceneStage.HOPE;
                        sceneStageTime = sceneStageTimes[2];
                        StartCoroutine(ChangeSkyBoxColour(2.0f));
                        Instantiate(hopeTreePrefab, hopeTreeSpawn.transform.position, hopeTreePrefab.transform.rotation);
                        Camera.main.GetComponent<CameraRaycast>().enabled = false;
                        StartCoroutine(Narration.instance.PlayScene3());

                        break;
                    }
                case SceneStage.HOPE:
                    {
                        if(!nextSceneLoading)
                        {
                            SFX.instance.StopForestSounds(5);
                            SFX.instance.StopWindSounds(5);
                            SFX.instance.StartOutroSounds(10);
                            Camera.main.GetComponent<OVRScreenFade>().FadeOut(5, SceneType.OUTRO);
                            nextSceneLoading = true;
                        }
                        break;
                    }
            }
        }
        sceneStageTime -= Time.deltaTime;
    }


    IEnumerator ChangeSkyBox(float time)
    {
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Camera.main.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }

    IEnumerator ChangeSkyBoxColour(float time)
    {
        startColour = Camera.main.GetComponent<Camera>().backgroundColor;
        float startTime = time;
        while (time > 0.0f)
        {
            Camera.main.GetComponent<Camera>().backgroundColor = Color.Lerp(Color.black, startColour, time/startTime);

            time -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ChangeDirectionalLight(float time)
    {
        float startTime = time;
        while (time > 0.0f)
        { 
            dirLight.GetComponent<Light>().intensity = Mathf.Lerp(0.0f, 1.0f, time / startTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
