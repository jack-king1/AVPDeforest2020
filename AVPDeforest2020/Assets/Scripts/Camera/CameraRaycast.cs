using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{

    static CameraRaycast instance;

    public static CameraRaycast Instance() { return instance; }

    GameObject lastHit;
    float lookTime = 0.0f;
    [SerializeField] float maxLookTime = 3.0f;
    public GameObject eyeLinePsPrefab;
    GameObject eyeLinePs;


    public float LookTime { get => lookTime; set => lookTime = value; }
    public float MaxLookTime { get => maxLookTime; set => maxLookTime = value; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        eyeLinePs = Instantiate(eyeLinePsPrefab);
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.forward * 3, transform.TransformDirection(Vector3.forward), out hit, 3000.0f))
        {
            eyeLinePs.transform.position = hit.point;
            //Debug.Log(eyeLinePs.transform.position);



            if (hit.collider.gameObject.tag != "Tree" && hit.collider.gameObject.tag != "Terrain" && !hit.collider.gameObject.GetComponent<Burnable>())
            {
                return;
            }

            if (hit.collider.gameObject.GetComponent<Burnable>().State != Burnable.States.ALIVE)
            {
                lookTime = 0.0f;
                return;
                //if (eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                //    eyeLinePs.GetComponent<ParticleSystem>().Stop();
            }

            if (lastHit && (lastHit == hit.collider.gameObject))
            {
                lookTime += Time.deltaTime;
                if (lookTime >= maxLookTime)
                {
                    if (eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                        eyeLinePs.GetComponent<ParticleSystem>().Stop();

                    FireManager.Instance().StartFire(hit.collider);
                }
            }
            else
            {
                lookTime = 0.0f;
                if (!eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                    eyeLinePs.GetComponent<ParticleSystem>().Play();
            }
            lastHit = hit.collider.gameObject;
        }
        else
        {
            eyeLinePs.transform.position = (transform.position + (transform.forward * 30.0f));
            //Debug.Log(eyeLinePs.transform.position);
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)* 3000.0f, Color.yellow);

    }
}
