using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using AudioManagerNS;

public class FireManager : MonoBehaviour
{
    static FireManager instance;
    public GameObject FireSoundPrefab;
    public List <GameObject> FireSoundPrefabs = new List<GameObject>();

    public static FireManager Instance() { return instance; }

    public GameObject psTree;
    public GameObject psTerrain;

    Burnable[] burnables;

    public int unburnedObjectCount = 0;

    private void Awake()
    {
        instance = this;
        GetBurnables();

        Debug.Log(burnables.Length);
        for(int i = 0; i < burnables.Length; ++i)
        {
            if(burnables[i].type == Burnable.Object.TERRAIN ||
                burnables[i].type == Burnable.Object.TRUNK)
            {
                ++unburnedObjectCount;
                Debug.Log(unburnedObjectCount);
            }
        }
    }

    public void RemoveFireSound(GameObject fireSound)
    {
        if(FireSoundPrefabs.Contains(fireSound))
        {
            FireSoundPrefabs.Remove(fireSound);
            Destroy(fireSound);
        }
    }


    private void Update()
    {
        if (MainSceneManager.instance.burnPercentFull == false)
        {
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Fire, 175 + MainSceneManager.instance.burnPercent*2 );
            Debug.Log("Hey;");
        }

        if (MainSceneManager.instance.burnPercentFull == true)
        {
          //  AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Fire, 0.1f / 1000);
            
        }

    }
    public void DecrementUnburnedObject()
    {
        Debug.Log(unburnedObjectCount);
        unburnedObjectCount--;
    }

    public void StartFire(Collider hit)
    {
        if (hit.gameObject.tag == "Tree" || hit.gameObject.tag == "Terrain")
        {
            if (hit.gameObject.GetComponent<Burnable>() && hit.gameObject.GetComponent<Burnable>().State == Burnable.States.ALIVE)
            {
                hit.gameObject.GetComponent<Burnable>().StartFire();

                //flamestrike - Fire 1 - Fire 4
                AudioSource flamestrike = AudioManager.Instance.Play(SFX.Instance.GetSFX("Flamestrike"), hit.gameObject.transform, 1, 1, AudioManager.AudioChannel.Fire);
                flamestrike.spatialBlend = 1;
                flamestrike.volume = 1;
                flamestrike.minDistance = 1;
                flamestrike.maxDistance = 2000;

               

                AudioSource fire1 = AudioManager.Instance.Play(SFX.Instance.GetSFX("Fire1"), hit.gameObject.transform, 1, 1, AudioManager.AudioChannel.Fire);
                fire1.spatialBlend = 1;
                fire1.volume = 1;
                fire1.minDistance = 1;
                fire1.maxDistance = 2000;

                AudioSource fire4 =  AudioManager.Instance.Play(SFX.Instance.GetSFX("Fire4"), hit.gameObject.transform, 1, 1, AudioManager.AudioChannel.Fire);
                fire4.spatialBlend = 1;
                fire4.volume = 1;
                fire4.minDistance = 1;
                fire4.maxDistance = 2000;

            }
        }
    } 

    public void GetBurnables()
    {
        burnables = FindObjectsOfType<Burnable>();
        StartCoroutine(GetNeighbours());
    }


    IEnumerator GetNeighbours()
    {

        for (int i = 0; i < burnables.Length; ++i)
        {
            if (burnables[i].type != Burnable.Object.TERRAIN)
                continue;

            for (int j = 0; j < burnables.Length; ++j)
            {
                if (burnables[j].type != Burnable.Object.TERRAIN || i == j)
                    continue;

                if (Connected(burnables[i].gameObject, burnables[j].gameObject))
                    burnables[i].Neighbours.Add(burnables[j].gameObject);
            }
        }


        yield return 0;
    }

    bool Connected(GameObject a, GameObject b)
    {
        int connectedAmount = 0;
        var aVerts = a.GetComponent<MeshFilter>().mesh.vertices;
        var bVerts = b.GetComponent<MeshFilter>().mesh.vertices;
        for (int i  = 0; i < aVerts.Length; ++i)
        {
            for (int j = 0; j < bVerts.Length; ++j)
            {

                if (aVerts[i] == bVerts[j])
                    connectedAmount++;

                if (connectedAmount >= 2)
                    return true;

            }
        }

        return false;
    }

}
