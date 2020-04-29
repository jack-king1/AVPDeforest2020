using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerKing : MonoBehaviour
{
    public static SceneManagerKing instance;
    int sceneCount = 0;
    public Image black;
    public Animator anim;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        sceneCount = 0;
    }

    public void LoadNextScene()
    {
        sceneCount++;
    }

    public void FadeToLevel()
    {
        sceneCount++;
        anim.SetTrigger("FadeOut");
    }
    
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneCount);
    }
}
