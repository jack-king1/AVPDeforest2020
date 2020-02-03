using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnLeaves : BurnTree
{
    [SerializeField] float burnRate = 1.0f;
    [SerializeField] float fadeRate = 1.0f;
    [SerializeField] float colourLife = 80.0f;

    // Start is called before the first frame update
    void Awake()
    {
        maxScale = GetComponent<Transform>().localScale;
        minScale = new Vector3(0.2f, 0.2f, 0.2f);
        startColour = GetComponent<MeshRenderer>().material.color;
        endColour = Color.black;
        //leafBurnRate = 5.0f;
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
            if (life >= 20.0f)
            {
                life -= Time.deltaTime * burnRate;
                colourLife -= Time.deltaTime * burnRate;
                //life -= rate;
                burnLeaves();
            }
            if(life <= 20.0f)
            {
                fadeLeaves();
            }
        }
    }

    void burnLeaves()
    {
        //Vector3 scale = GetComponentInParent<Transform>().parent.localScale;
        //scale.x = Mathf.Lerp(minScale.x, maxScale.x, life / 100.0f);
        //scale.y = Mathf.Lerp(minScale.y, maxScale.y, life / 100.0f);
        //scale.z = Mathf.Lerp(minScale.z, maxScale.z, life / 100.0f);
        //GetComponentInParent<Transform>().parent.localScale = scale;

        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x = Mathf.Lerp(minScale.x, maxScale.x, life / 100.0f);
        scale.y = Mathf.Lerp(minScale.y, maxScale.y, life / 100.0f);
        scale.z = Mathf.Lerp(minScale.z, maxScale.z, life / 100.0f);
        GetComponent<Transform>().localScale = scale;

        if(colourLife < 0.0f)
            return;

        var colour = GetComponent<MeshRenderer>().material.color;
        colour.r = Mathf.Lerp(endColour.r, startColour.r, colourLife / 80.0f);
        colour.g = Mathf.Lerp(endColour.g, startColour.g, colourLife / 80.0f);
        colour.b = Mathf.Lerp(endColour.b, startColour.b, colourLife / 80.0f);
        GetComponent<MeshRenderer>().material.color = colour;
    }

    void fadeLeaves()
    {
        if (GetComponent<MeshRenderer>().material.color.a > 0.0f)
        {
            var colour = GetComponent<MeshRenderer>().material.color;
            colour.a -= Time.deltaTime * fadeRate;
            if (colour.a < 0.0f)
                colour.a = 0.0f;
            GetComponent<MeshRenderer>().material.color = colour;
        }
    }
}
