using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempAudioFixIdle : MonoBehaviour
{
    private void Update()
    {
        if(SFX.instance != null)
        {
            SFX.instance.StopOutroSounds(1);
            Destroy(this);
        }
    }
}
