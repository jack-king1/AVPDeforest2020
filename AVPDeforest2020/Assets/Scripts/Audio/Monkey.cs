using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public static Monkey instance;

    bool stopMonkeys = false;
  
    private AudioSource monkeys;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMonkeys)
        {
            monkeys.volume -= Time.deltaTime / 50;
        }

        
    }

    

    #region MonkeySounds
    public void MonkeySounds()
    {
        monkeys = AudioManagerNS.AudioManager.Instance.PlayLoop(SFX.Instance.GetSFX("Monkey"), transform, .5f, 1,  AudioManagerNS.AudioManager.AudioChannel.Jungle);
        monkeys.spatialBlend = 1f;
        monkeys.minDistance = 50;
        monkeys.maxDistance = 2000;
    }

    public void StopMonkey()
    {
        stopMonkeys = true;
    }

    #endregion
}
