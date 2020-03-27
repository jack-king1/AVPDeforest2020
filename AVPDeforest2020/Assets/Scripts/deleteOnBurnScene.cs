using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteOnBurnScene : MonoBehaviour
{

    void Start()
    {
        MainSceneManager.instance.fireStartedDelegate += Deleteobject;
    }

    void Deleteobject()
    {
        Destroy(this.gameObject);
    }

}
