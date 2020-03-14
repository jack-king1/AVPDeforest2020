using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using yaSingleton;

public enum SceneType
{
    IDLE = 0,
    INTRO,
    MAIN,
    OUTRO
}

[CreateAssetMenu(fileName = "Scene Manager", menuName = "Singletons/SceneManager")]
public class ScenesManager : Singleton<ScenesManager>
{
    static ScenesManager instance;
    SceneType activeScene = SceneType.IDLE;

    public SceneType ActiveScene { get => activeScene; set => activeScene = value; }

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(SceneType sceneType)
    {
        if (activeScene == SceneType.MAIN)
        {
            if (FireManager.Instance())
                FireManager.Instance().GetBurnables();
        }
        SceneManager.LoadScene((int)sceneType);
    }
}
