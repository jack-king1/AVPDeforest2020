using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
    static FireManager instance;
    public GameObject FireSoundPrefab;
    public List <GameObject> FireSoundPrefabs = new List<GameObject>();


    public static FireManager Instance() { return instance; }

    private void Awake()
    {
        instance = this;
        GetBurnables();
    }

    public GameObject psTree;
    public GameObject psTerrain;

    Burnable[] burnables;

    public void RemoveFireSound(GameObject fireSound)
    {
        if(FireSoundPrefabs.Contains(fireSound))
        {
            FireSoundPrefabs.Remove(fireSound);
            Destroy(fireSound);
        }
    }

    public void StartFire(Collider hit)
    {
        if (hit.gameObject.tag == "Tree" || hit.gameObject.tag == "Terrain")
        {
            if (hit.gameObject.GetComponent<Burnable>() && hit.gameObject.GetComponent<Burnable>().State == Burnable.States.ALIVE)
            {
                hit.gameObject.GetComponent<Burnable>().StartFire();

                GameObject newFireSound = Instantiate(FireSoundPrefab, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity, hit.transform);
                FireSoundPrefabs.Add(newFireSound);
             
                hit.gameObject.GetComponent<Burnable>().fireSound = newFireSound;
            }
        }
    }

    public void GetBurnables()
    {
        StartCoroutine(GetNeighbours());
    }


    IEnumerator GetNeighbours()
    {
        burnables = FindObjectsOfType<Burnable>();

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
