﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

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

    public delegate void BurningStartedDelegate();
    public BurningStartedDelegate fireStartedDelegate;

    public delegate void ForestBurnProgress();
    public ForestBurnProgress fireBurnProgressDelegate;
    public delegate void SilenecSceneStarted();
    public SilenecSceneStarted silenceSceneStartedDelegate;

    public float burnPercent = 0;
    public bool burnPercentFull = false;

    public enum SceneStage
    {
        TRANQUIL = 0,
        BURNING = 1,
        SILENCE = 2,
        HOPE = 3
    }

    SceneStage currentStage = SceneStage.TRANQUIL;

    [SerializeField]float[] sceneStageTimes = new float[4];
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
                        if(fireStartedDelegate != null)
                        {
                            fireStartedDelegate(); //Call all functions that are subscribed to this.
                        }
                        currentStage = SceneStage.BURNING;
                        sceneStageTime = sceneStageTimes[1];
                        AnimalManager.instance.RemoveAllAnimals();
                        ForestAudio.instance.StopJungle();
                        StartCoroutine(Narration.instance.FireNarration());
                      //  Narration.instance.StartCoroutine(Narration.instance.FireNarration());
                        StartCoroutine(ChangeSkyBox(5.0f));
                        StartCoroutine(ChangeDirectionalLight(2.0f));
                        Camera.main.GetComponent<CameraRaycast>().enabled = true;
                       // StartCoroutine(Narration.instance. PlayScene2());
                        break;
                    }
                case SceneStage.BURNING:
                    {
                        currentStage = SceneStage.SILENCE;
                   
                        sceneStageTime = sceneStageTimes[2];
                        ForestAudio.instance.StopFire();
                        
                        StartCoroutine(ChangeSkyBoxColour(2.0f));
                        Camera.main.GetComponent<CameraRaycast>().enabled = false;


                        break;
                    }
                case SceneStage.SILENCE:
                    {
                        currentStage = SceneStage.HOPE;
                        silenceSceneStartedDelegate();
                        sceneStageTime = sceneStageTimes[3];
                        ForestAudio.instance.StartHope();
                        Instantiate(hopeTreePrefab, hopeTreeSpawn.transform.position, hopeTreePrefab.transform.rotation);
                         StartCoroutine(Narration.instance.HopeNarration());
                        break;
                    }
                case SceneStage.HOPE:
                    {
                        if(!nextSceneLoading)
                        {
                            //SFX.Instance.StopForestSounds(5);
                            //SFX.Instance.StopWindSounds(5);
                            //SFX.Instance.StartOutroSounds(10);
                            //ForestAudio.instance.StopHope();
                            Camera.main.GetComponent<OVRScreenFade>().FadeOut(5, SceneType.OUTRO);
                            nextSceneLoading = true;
                        }
                        break;
                    }
            }
        }
        sceneStageTime -= Time.deltaTime;
    }

    public void BurnCounterIncrease()
    {
        if(!burnPercentFull)
        {
            burnPercent += 0.0025f;
           // Debug.Log("Fire Burn progress:" + burnPercent);
            if (burnPercent >= 1)
            {
                burnPercentFull = true;
                fireBurnProgressDelegate?.Invoke();
            }
        }
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
