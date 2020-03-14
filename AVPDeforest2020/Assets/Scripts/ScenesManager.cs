using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    IDLE = 0,
    INTRO,
    MAIN,
    OUTRO
}

public class ScenesManager : MonoBehaviour
{
    static ScenesManager instance;

    public static ScenesManager Instance() { return instance; }
    public Camera VRcam;



    SceneType activeScene = SceneType.IDLE;

    public SceneType ActiveScene { get => activeScene; set => activeScene = value; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(VRcam == null)
        {
            VRcam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(SceneType sceneType)
    {
        if (activeScene == SceneType.MAIN)
        {
            if (FireManager.Instance())
                FireManager.Instance().GetBurnables();
        }

        Debug.Log("Loading " + sceneType.ToString()); ;
        SceneManager.LoadScene((int)sceneType);
        if(VRcam == null)
        {
            VRcam = Camera.main;
        }
        VRcam = Camera.main;
        VRcam.GetComponent<OVRScreenFade>().FadeIn(5, sceneType);
    }

}
