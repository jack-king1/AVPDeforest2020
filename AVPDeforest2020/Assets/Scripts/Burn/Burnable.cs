﻿using System.Collections;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    [HideInInspector] public GameObject ps;

    class Colour
    {
        public float changeRate = 0.0f;
        public float life = 0.0f;
        public Color start;
        public Color end;
    }

    Colour colour = new Colour();

    public enum Object
    {
        TRUNK = 0,
        LEAVES = 1,
        TERRAIN = 2
    }

    public enum States
    {
        ALIVE = 0,
        BURN = 1,
        DEAD = 2
    }

    public Object type = Object.TRUNK;
    States state = States.ALIVE;

    public States State { get { return state; } set { state = value; } }

    float health = 100.0f;

    Vector3 maxScale;
    Vector3 minScale;

    [SerializeField] float burnRate = 1.0f;
    [SerializeField] float fadeRate = 1.0f;
    [SerializeField] float fadeLife = 100.0f;

    bool destroyedFire = false;


    // Start is called before the first frame update
    void Update()
    {
        if(state == States.DEAD && !destroyedFire)
        {
            StartCoroutine(EndFire());
        }
    }

    public void Burn()
    {
        if (state != States.ALIVE)
        {
            return;
        }

        state = States.BURN;
        switch (type)
        {
            case Object.TRUNK:
                {
                    maxScale = GetComponent<Transform>().localScale;
                    minScale = maxScale * 0.8f;
                    colour.start = gameObject.GetComponent<MeshRenderer>().material.color;
                    colour.end = Color.black;
                    StartCoroutine(BurnTrunk());
                    break;
                }
            case Object.LEAVES:
                {
                    maxScale = GetComponent<Transform>().localScale;
                    minScale = maxScale * 0.2f;
                    colour.start = gameObject.GetComponent<MeshRenderer>().material.color;
                    colour.end = Color.black;
                    StartCoroutine(BurnLeaves());
                    break;
                }
            case Object.TERRAIN:
                {
                    colour.start = gameObject.GetComponent<MeshRenderer>().material.color;
                    colour.end = new Color(43.0f / 255.0f, 24.0f / 255.0f, 10.0f / 255.0f);
                    StartCoroutine(BurnTerrain());
                    break;
                }
        }
    }

    IEnumerator BurnTrunk()
    {
        Vector3 scale = GetComponent<Transform>().localScale;

        var col = gameObject.GetComponent<MeshRenderer>().material.color;

        while (health > 0.0f)
        {
            scale = Vector3.Lerp(minScale, maxScale, health / 100.0f);
            GetComponent<Transform>().localScale = scale;


            col = Color.Lerp(colour.end, colour.start, health / 100.0f);
            gameObject.GetComponent<MeshRenderer>().material.color = col;

            health -= Time.deltaTime * burnRate;

            yield return null;
        }

        state = States.DEAD;
    }

    IEnumerator BurnLeaves()
    {
        Vector3 scale = gameObject.GetComponent<Transform>().localScale;

        var col = gameObject.GetComponent<MeshRenderer>().material.color;

        while (health >= 0.0f)
        {

            scale = Vector3.Lerp(minScale, maxScale, health / 100.0f);
            gameObject.GetComponent<Transform>().localScale = scale;

            col = Color.Lerp(colour.end, colour.start, (health - 20.0f) / 80.0f);
            gameObject.GetComponent<MeshRenderer>().material.color = col;

            health -= Time.deltaTime * burnRate;

            yield return null;
        }

        while (fadeLife > 0.0f)
        {
            col.a = Mathf.Lerp(0.0f, 1.0f, fadeLife / 100.0f);
            if (col.a < 0.1f)
                col.a = 0.0f;

            gameObject.GetComponent<MeshRenderer>().material.color = col;

            fadeLife -= Time.deltaTime * fadeRate;

            yield return null;
        }

        state = States.DEAD;
    }

    IEnumerator BurnTerrain()
    {
        var col = gameObject.GetComponent<MeshRenderer>().material.color;

        while (health >= 0.0f)
        {

            col = Color.Lerp(colour.end, colour.start, health / 100.0f);
            gameObject.GetComponent<MeshRenderer>().material.color = col;

            health -= Time.deltaTime * burnRate;

            yield return null;
        }

        state = States.DEAD;
    }

    IEnumerator EndFire()
    {
        destroyedFire = true;

        while (ps.GetComponent<ParticleSystem>().isPlaying)
        {
            ps.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);

            yield return null;
        }

        Destroy(ps);
    }
}
