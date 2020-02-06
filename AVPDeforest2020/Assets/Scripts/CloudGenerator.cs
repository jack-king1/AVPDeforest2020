using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    
    [SerializeField] private int maxClouds;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float cloudSpawnDelay;
    [SerializeField] private GameObject[] cloudPrefabs;
    [SerializeField] private List<GameObject> cloudPool;

    private float timer;

    private void Start()
    {
        cloudPool = new List<GameObject>();
        timer = cloudSpawnDelay;
    }

    void Update()
    {
        if(cloudPool != null)
        {
            foreach(GameObject cloud in cloudPool)
            {
                cloud.GetComponent<move>().Move();
            }
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(cloudPool.Count < maxClouds)
            {
                CreateCloud();
                timer = cloudSpawnDelay;
            }

        }
    }

    void CreateCloud()
    {
        int id = Random.Range(0, cloudPrefabs.Length);
        Vector3 position = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y,
            Random.Range(gameObject.transform.position.z - 200, gameObject.transform.position.z + 200));
        GameObject newCloud = Instantiate(cloudPrefabs[id], position, Quaternion.identity);
        newCloud.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.0f);
        newCloud.GetComponent<move>().Init(Random.Range(minSpeed, maxSpeed));

        StartCoroutine(EffectsManager.FadeAlpha(result => newCloud.GetComponent<MeshRenderer>().material.color = result, 15.0f,
            newCloud.GetComponent<MeshRenderer>().material.color.a, 1f));
        cloudPool.Add(newCloud);
    }
}
