using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    bool usingVr = false;

    public GameObject VrCamera;
    public GameObject PcCamera;
    public GameObject hopeTreePrefab;

    GameObject hopeTreeSpawn;
    GameObject dirLight;
    Color startColour = new Color();

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

        hopeTreeSpawn = GameObject.FindGameObjectWithTag("HopeTreeSpawn");
        dirLight = GameObject.FindGameObjectWithTag("DirectinalLight");

        if (!usingVr)
        {
            PcCamera.GetComponent<CameraRaycast>().enabled = false;
            PcCamera.GetComponent<CameraMovement>().enabled = true;
        }
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
                        sceneStageTime = 90.0f;

                        StartCoroutine(ChangeSkyBox(5.0f));
                        StartCoroutine(ChangeDirectionalLight(90.0f));
                        if (usingVr)
                        {
                            VrCamera.GetComponent<CameraRaycast>().enabled = true;
                        }
                        else
                        {
                            PcCamera.GetComponent<CameraRaycast>().enabled = true;

                        }
                        SFX.instance.JungleSounds();
                        StartCoroutine(Narration.instance. PlayScene2());
                        break;
                    }
                case SceneStage.BURNING:
                    {
                        currentStage = SceneStage.HOPE;
                        sceneStageTime = 30.0f;
                        StartCoroutine(ChangeSkyBoxColour(2.0f));
                        Instantiate(hopeTreePrefab, hopeTreeSpawn.transform.position, hopeTreePrefab.transform.rotation);
                        if (usingVr)
                        {
                            VrCamera.GetComponent<CameraRaycast>().enabled = false;
                        }
                        else
                        {
                            PcCamera.GetComponent<CameraRaycast>().enabled = false;
                        }
                        StartCoroutine(Narration.instance.PlayScene3());

                        break;
                    }
                case SceneStage.HOPE:
                    {
                        ScenesManager.Instance().LoadNextScene();
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
        VrCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }

    IEnumerator ChangeSkyBoxColour(float time)
    {
        startColour = VrCamera.GetComponent<Camera>().backgroundColor;
        float startTime = time;
        while (time > 0.0f)
        {
            VrCamera.GetComponent<Camera>().backgroundColor = Color.Lerp(Color.black, startColour, time/startTime);

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
