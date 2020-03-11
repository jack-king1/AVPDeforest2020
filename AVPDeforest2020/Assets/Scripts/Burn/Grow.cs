using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{

    bool grow = true;
    [SerializeField][Range(-10.0f, 10.0f)] float growRate = 2.0f;
    float health = 0.0f;
    Color startColour = new Color();
    Color endColour = new Color();

    Vector3 startSize = new Vector3();
    Vector3 endSize = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        var col = gameObject.GetComponent<MeshRenderer>().material.color;

        startColour = Color.black;
        endColour = col;
        col = Color.black;
        gameObject.GetComponent<MeshRenderer>().material.color = col;
        startSize = new Vector3(0.0f, 0.0f, 0.0f);
        endSize = transform.localScale;

        transform.localScale = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(grow)
        {
            var col = gameObject.GetComponent<MeshRenderer>().material.color;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColour, endColour, health / 100.0f);


            var scale = transform.localScale;
            scale = Vector3.Lerp(startSize, endSize, health / 100.0f);
            transform.localScale = scale;
            health += Time.deltaTime * growRate;
        }
    }
}
