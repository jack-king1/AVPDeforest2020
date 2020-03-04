using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    static CameraMovement instance;

    public static CameraMovement Instance() { return instance; }

    float xSpeed = 2.0f;
    float ySpeed = 2.0f;

    float yaw = .0f;
    float pitch = 0.0f;

    [SerializeField] float fadeRate = 1.0f;
    [SerializeField] float shrinkRate = 1.0f;
    GameObject lastHit;
    float lookTime = 0.0f;
    [SerializeField] float maxLookTime = 3.0f;
    public GameObject eyeLinePsPrefab;
    GameObject eyeLinePs;


    public float LookTime { get => lookTime; set => lookTime = value; }
    public float MaxLookTime { get => maxLookTime; set => maxLookTime = value; }

    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        eyeLinePs = Instantiate(eyeLinePsPrefab);
    }

    void Update()
    {
        yaw += xSpeed * Input.GetAxis("Mouse X");
        pitch -= ySpeed * Input.GetAxis("Mouse Y");


        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3000.0f))
        {
            eyeLinePs.transform.position = hit.point;
            Debug.Log(eyeLinePs.transform.position);

            if (hit.collider.gameObject.tag == "Tree" || hit.collider.gameObject.tag == "Terrain")
            {

                if(hit.collider.gameObject.GetComponent<Burnable>().State != Burnable.States.ALIVE)
                {
                    if (eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                        eyeLinePs.GetComponent<ParticleSystem>().Stop();
                }

                if (lastHit && (lastHit == hit.collider.gameObject))
                {
                    lookTime += Time.deltaTime;
                    if (lookTime >= maxLookTime)
                    {
                        if(eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                            eyeLinePs.GetComponent<ParticleSystem>().Stop();

                        FireManager.Instance().SpawnFire(hit.collider);
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
        }
        else
        {
            if (eyeLinePs.GetComponent<ParticleSystem>().isPlaying)
                eyeLinePs.GetComponent<ParticleSystem>().Stop();
        }


        //MultipleHits();
       // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3000.0f, Color.yellow);
    }

    private void MultipleHits()
    {
        RaycastHit[] hits;

        // Does the ray intersect any objects excluding the player layer
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 3000.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.tag == "Tree")
            {
                if (hit.collider.gameObject.GetComponent<Renderer>().enabled)
                {
                    var mat = hit.collider.gameObject.GetComponent<MeshRenderer>().materials;
                    for (int j = 0; j < mat.Length; ++j)
                    {
                        if (mat[j].color != Color.black)
                        {
                            var colour = mat[j].color;
                            colour.r -= Time.deltaTime * fadeRate;
                            colour.g -= Time.deltaTime * fadeRate;
                            colour.b -= Time.deltaTime * fadeRate;
                            mat[j].color = colour;
                            hit.collider.gameObject.GetComponent<MeshRenderer>().materials[j] = mat[j];
                            Vector3 scale = hit.collider.gameObject.GetComponent<Transform>().localScale;
                            scale.x -= Time.deltaTime * shrinkRate;
                            scale.y -= Time.deltaTime * shrinkRate;
                            scale.z -= Time.deltaTime * shrinkRate;
                            hit.collider.gameObject.GetComponent<Transform>().localScale = scale;
                        }
                        else
                        {
                            var colour = mat[j].color;
                            colour = Color.black;
                            mat[j].color = colour;
                            hit.collider.gameObject.GetComponent<MeshRenderer>().materials[j] = mat[j];
                            Destroy(hit.collider.gameObject);
                            break;
                        }
                    }
                }
                break;
            }
            //Debug.Log(hit.collider.gameObject);
        }
    }
}
