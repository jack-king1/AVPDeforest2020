using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayTransition : MonoBehaviour
{
    public float neededLookTime;
    [SerializeField] private float progress;
    public float beginRayTimer;


    // Start is called before the first frame update
    void Start()
    {
        progress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(beginRayTimer > 0)
        {
            beginRayTimer -= Time.deltaTime;
        }
        else
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
            if (Physics.Raycast(transform.position + transform.forward * 3, transform.TransformDirection(Vector3.forward), out hit, 3000.0f))
            {
                if (hit.collider.CompareTag("TransitionTree"))
                {
                    Debug.Log("Tree Hit");
                    if (progress >= 1)
                    {
                        ScenesManager.Instance().LoadNextScene();

                    }
                    else
                    {
                        progress += 0.5f * Time.deltaTime;
                        GetComponent<OVRScreenFade>().SetFadeLevel(progress);
                    }
                }
            }
            else
            {
                if (progress > 0)
                {
                    progress -= 0.5f * Time.deltaTime;
                    GetComponent<OVRScreenFade>().SetFadeLevel(progress);
                }
            }
        }
    }
}
