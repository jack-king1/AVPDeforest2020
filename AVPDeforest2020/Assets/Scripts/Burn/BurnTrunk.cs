using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTrunk : BurnTree
{
    [SerializeField] protected float trunkBurnRate = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        maxScale = GetComponent<Transform>().localScale;
        minScale = new Vector3(0.8f, 0.8f, 0.8f);
        startColour = GetComponent<MeshRenderer>().material.color;
        endColour = Color.black;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (burnTime >= 0.0f)
        {
            burnTime -= Time.deltaTime;
            if (life >= 0)
            {
                life -= Time.deltaTime * trunkBurnRate;
                burnTrunk();
            }
        }
    }

    void burnTrunk()
    {
        Vector3 scale = GetComponentInParent<Transform>().localScale;
        scale.x = Mathf.Lerp(minScale.x, maxScale.x, life / 100.0f);
        scale.y = Mathf.Lerp(minScale.y, maxScale.y, life / 100.0f);
        scale.z = Mathf.Lerp(minScale.z, maxScale.z, life / 100.0f);
        GetComponentInParent<Transform>().localScale = scale;

        var colour = GetComponent<MeshRenderer>().material.color;
        colour.r = Mathf.Lerp(endColour.r, startColour.r, life / 100.0f);
        colour.g = Mathf.Lerp(endColour.g, startColour.g, life / 100.0f);
        colour.b = Mathf.Lerp(endColour.b, startColour.b, life / 100.0f);
        GetComponent<MeshRenderer>().material.color = colour;
    }
}
