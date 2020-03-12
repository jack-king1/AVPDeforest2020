using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static CameraManager instance;

    public GameObject introScenePosition;
    public GameObject forestScenePosition;
    public GameObject outroScenePosition;

    public static CameraManager Instance() { return instance; }


    public GameObject camera;


    List<Vector3> scenePositions = new List<Vector3>();


    private void Awake()
    {
        instance = this;
        scenePositions.Add(introScenePosition.transform.position);
        scenePositions.Add(forestScenePosition.transform.position);
        scenePositions.Add(outroScenePosition.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCameraScene(ScenesManager.Scene scene)
    {
        //camera.transform.position = scenePositions[(int)scene];
    }
    
}
