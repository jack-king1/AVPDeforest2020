using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteOnBurnScene : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitToAdd());
    }

    void Deleteobject()
    {
        Destroy(this.gameObject);
    }

    IEnumerator WaitToAdd()
    {
        yield return new WaitForSeconds(1);
        MainSceneManager.instance.fireStartedDelegate += Deleteobject;
    }
}
