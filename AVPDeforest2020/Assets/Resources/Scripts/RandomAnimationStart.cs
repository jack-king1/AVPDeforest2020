using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{

    private float min, max;
    Animator anim;

    public Color[] birdColors;
    MeshRenderer[] mf;

    void Start()
    {
        min = 0.9f;
        max = 1f;

        mf = GetComponentsInChildren<MeshRenderer>();

        int i = Random.Range(0, birdColors.Length);

        foreach (var f in mf)
        {     
            f.material.color = birdColors[i];
        }

       // anim = GetComponent<Animator>();
        //anim.SetFloat("animspeed", Random.Range(min, max));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
