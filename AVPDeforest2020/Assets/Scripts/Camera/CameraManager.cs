using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static CameraManager instance;

    public static CameraManager Instance() { return instance; }


    public GameObject camera;


    List<Vector3> scenePositions = new List<Vector3>();

    public enum Components
    {
        ZOOM = 0,
        RAYCAST = 1
    }


    private void Awake()
    {
        instance = this;
        scenePositions.Add(new Vector3(0, 202, 2095));
        scenePositions.Add(new Vector3(188.42f, 17f, 129.91f));
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
        camera.transform.position = scenePositions[(int)scene];


        switch (scene)
        {
            case ScenesManager.Scene.INTRO:
                {
                    SetComponent(Components.ZOOM, true);
                    SetComponent(Components.RAYCAST, false);
                    break;
                }
            case ScenesManager.Scene.MAIN:
                {
                    SetComponent(Components.ZOOM, false);
                    SetComponent(Components.RAYCAST, true);
                    break;
                }
            case ScenesManager.Scene.OUTRO:
                {
                    SetComponent(Components.ZOOM, true);
                    SetComponent(Components.RAYCAST, false);
                    break;
                }
        }

    }

    public void SetComponent(Components comp, bool enable = true)
    {
        switch (comp)
        {
            case Components.ZOOM:
                {
                    if(camera.GetComponent<ZoomToObject>())
                        camera.GetComponent<ZoomToObject>().enabled = enable;
                    break;
                }
            case Components.RAYCAST:
                {
                    if (camera.GetComponent<CameraRaycast>())
                        camera.GetComponent<CameraRaycast>().enabled = enable;
                    break;
                }
        }
    }

    
}
