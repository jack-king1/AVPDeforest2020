using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    static ScenesManager instance;

    public static ScenesManager Instance() { return instance; }

    public GameObject[] scenes = new GameObject[3];
    public GameObject VRcam;

    public enum Scene
    {
        INTRO = 0,
        MAIN = 1,
        OUTRO = 2
    }

    Scene activeScene = 0;


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

                if(scene == Scene.MAIN)
                {
                    FireManager.Instance().GetBurnables();
                }
            }
            else
                scenes[i].SetActive(false);
        }

    }

    public void LoadNextScene()
    {
        activeScene++;
        switch(activeScene)
        {
            case Scene.INTRO:
                Debug.Log("Loading Intro");
                SceneManager.LoadScene(0);
                VRcam.GetComponent<OVRScreenFade>().FadeIn(5);
                break;
            case Scene.MAIN:
                Debug.Log("Loading Forest Scene");
                SceneManager.LoadScene(1);
                VRcam.GetComponent<OVRScreenFade>().FadeIn(5);
                break;
            case Scene.OUTRO:
                Debug.Log("Loading outro");
                SceneManager.LoadScene(2);
                VRcam.GetComponent<OVRScreenFade>().FadeIn(5);
                break;
            default:
                break;
        }
    }
}
