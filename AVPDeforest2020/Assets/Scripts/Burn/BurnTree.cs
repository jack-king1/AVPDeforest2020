using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTree : MonoBehaviour
{

    [SerializeField] protected float life = 100.0f;
    [SerializeField] protected bool burn = false;
    [SerializeField] bool dead = false;

   [SerializeField] protected Vector3 maxScale;
   [SerializeField] protected Vector3 minScale;
   [SerializeField] protected Color startColour;
   [SerializeField] protected Color endColour;
   public float burnTime = 5.0f;

    //[SerializeField] GameObject trunk;
    //[SerializeField] GameObject leaves;

    Color burntCol = Color.black;

    public bool Burn { get => burn; set => burn = value; }

    // Start is called before the first frame update
    void Awake()
    {
        //List<Transform> transforms = new List<Transform>(this.GetComponentsInChildren<Transform>());
        //foreach(Transform t in transforms)
        //{
        //    if(t.gameObject.tag == "Trunk")
        //        trunk = t.gameObject;

        //    else if (t.gameObject.tag == "Leaf")
        //        leaves = t.gameObject;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            this.enabled = false;
        //if(burn)
        //    Burn();
    }

    //void Burn()
    //{

    //    Vector3 scale = GetComponent<Transform>().localScale;
    //    scale.x -= Time.deltaTime * shrinkRate;
    //    scale.y -= Time.deltaTime * shrinkRate;
    //    scale.z -= Time.deltaTime * shrinkRate;
    //    GetComponent<Transform>().localScale = scale;

    //    BurnLeaves();
    //    BurnTrunk();
    //}

    //void BurnLeaves()
    //{
    //    Vector3 scale = leaves.GetComponent<Transform>().localScale;
    //    scale.x -= Time.deltaTime * shrinkRate;
    //    scale.y -= Time.deltaTime * shrinkRate;
    //    scale.z -= Time.deltaTime * shrinkRate;
    //    leaves.GetComponent<Transform>().localScale = scale;

    //    var colour = leaves.GetComponent<MeshRenderer>().material.color;
    //    colour.r -= Time.deltaTime * discolourRate;
    //    colour.g -= Time.deltaTime * discolourRate;
    //    colour.b -= Time.deltaTime * discolourRate;
    //    leaves.GetComponent<MeshRenderer>().material.color = colour;

    //}

    //void BurnTrunk()
    //{
    //    Vector3 scale = trunk.GetComponent<Transform>().localScale;
    //    scale.x -= Time.deltaTime * shrinkRate;
    //    scale.y -= Time.deltaTime * shrinkRate;
    //    scale.z -= Time.deltaTime * shrinkRate;
    //    trunk.GetComponent<Transform>().localScale = scale;

    //    var colour = trunk.GetComponent<MeshRenderer>().material.color;
    //    colour.r -= Time.deltaTime * discolourRate;
    //    colour.g -= Time.deltaTime * discolourRate;
    //    colour.b -= Time.deltaTime * discolourRate;
    //    trunk.GetComponent<MeshRenderer>().material.color = colour;
    //}
}
