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

    public IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(sceneCount);
    }
    
}
