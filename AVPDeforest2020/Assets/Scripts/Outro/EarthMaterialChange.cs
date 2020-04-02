using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMaterialChange : MonoBehaviour
{

    Color[] startColours = new Color[2];
    Color[] endColours = new Color[2];

    [SerializeField][Range(0.001f, 1.0f)]
    float regrowRate = 0.05f;
    float lerpTime = 0.0f;

    float delay = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        startColours[0] = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        startColours[1] = gameObject.GetComponent<MeshRenderer>().materials[1].color;
        endColours[0] = new Color(6.0f / 255.0f, 181.0f / 255.0f, 215.0f / 255.0f, 255.0f / 255.0f);
        endColours[1] = new Color(36.0f / 255.0f, 186.0f / 255.0f, 15.0f / 255.0f, 255.0f / 255.0f);
        //gameObject.GetComponent<MeshRenderer>().materials[1].color = gameObject.GetComponent<MeshRenderer>().materials[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay > 0.0f)
            return;

        var oceanColour = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        var landColour = gameObject.GetComponent<MeshRenderer>().materials[1].color;

        oceanColour = Color.Lerp(startColours[0], endColours[0], lerpTime);
        landColour = Color.Lerp(startColours[1], endColours[1], lerpTime);

        gameObject.GetComponent<MeshRenderer>().materials[0].color = oceanColour;
        gameObject.GetComponent<MeshRenderer>().materials[1].color = landColour;
   
        lerpTime += Time.deltaTime * regrowRate;

    }
}
