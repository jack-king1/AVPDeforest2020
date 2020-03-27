using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerNS;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    [SerializeField] bool usingVr = false;
    public GameObject CameraGO;
    public GameObject hopeTreePrefab;

    public GameObject fogPSParent;
    bool fogStopped = false;

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

    float burnPercent = 0;
    bool burnPercentFull = false;

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

    float fogTimer = 30.0f;

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
                        StartCoroutine(ChangeSkyBox(5.0f));
                        StartCoroutine(ChangeDirectionalLight(2.0f));
                        Camera.main.GetComponent<CameraRaycast>().enabled = true;
                        break;
                    }
                case SceneStage.BURNING:
                    {
                        if (FireManager.Instance().unburnedObjectCount > 5)
                            return;

                        currentStage = SceneStage.SILENCE;
                      //  Narration.instance.StartCoroutine(Narration.instance.PlayScene3());
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


        if (currentStage == SceneStage.BURNING)
        {
            if(fogTimer < 0.0f && !fogStopped)
            {
                foreach(var fog in fogPSParent.GetComponentsInChildren<ParticleSystem>())
                {
                    if(fog.isPlaying)
                    {
                        fog.Stop(true);
                    }
                }
                fogStopped = true;
            }
            fogTimer -= Time.deltaTime;
        }
    }

    public void burnCounterIncrease()
    {
        if(!burnPercentFull)
        {
            burnPercent += 0.05f;
            Debug.Log("Fire Burn progress:" + burnPercent);
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
            dirLight.GetComponent<Light>().intensity = Mathf.Lerp(0.5f, 1.0f, time / startTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
