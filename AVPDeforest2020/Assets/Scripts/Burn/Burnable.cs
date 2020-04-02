using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Burnable : MonoBehaviour
{   
    public GameObject ps;
    GameObject fire;

    class Colour
    {
        public float changeRate = 0.0f;
        public float life = 0.0f;
        public Color start;
        public Color end;
        public Color [] starts;
        public Color [] ends;
    }

    Colour colour = new Colour();

    public enum Object
    {
        TRUNK = 0,
        LEAVES = 1,
        TERRAIN = 2,
        FOLIAGE = 3,
        ROCK = 4
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

    public List<GameObject> Neighbours { get => neighbours; set => neighbours = value; }

    float health = 100.0f;

    Vector3 maxScale;
    Vector3 minScale;

    float burnRate = 1.0f;
    float fadeRate = 1.0f;
    float fadeLife = 100.0f;

    bool destroyedFire = false;

    float burnTime = 0.0f;
    private AudioFade fade;

    [SerializeField] private List<GameObject> neighbours = new List<GameObject>();

    private void Awake()
    {

        switch (type)
        {
            case Object.TRUNK:
                {
                    burnRate = 10.0f;
                    fadeRate = 50.0f;
                    break;
                }
            case Object.LEAVES:
            case Object.FOLIAGE:
                {
                    burnRate = 20.0f;
                    fadeRate = 100.0f;
                    break;
                }
            case Object.TERRAIN:
            case Object.ROCK:
                {
                    burnRate = 5.0f;
                    break;
                }
        }



        if (type == Object.TRUNK)
        {
            var burns = GetComponentsInChildren<Burnable>();

            for (int i = 0; i < burns.Length; ++i)
            {
                if(burns[i].name != name)
                    neighbours.Add(burns[i].gameObject);
            }
        }

    }

    // Start is called before the first frame update
    void Update()
    {  
        if (state == States.DEAD && !destroyedFire)
        {
            StopFire();
           // StartCoroutine(EndFire());
        }
        else if(state == States.BURN && type != Object.FOLIAGE || type != Object.LEAVES)
        {
            foreach (var n in neighbours)
            {
                var nBurnable = n.GetComponent<Burnable>();
                if (nBurnable)
                {
                    if (nBurnable.State == States.ALIVE && nBurnable.type == Object.LEAVES && burnTime > 2.0f)
                    {
                        nBurnable.StartFire();
                    }
                    else if (nBurnable.State == States.ALIVE && nBurnable.type == Object.TRUNK && burnTime > 3.0f)
                    {
                        nBurnable.StartFire();
                    }
                    else if (nBurnable.State == States.ALIVE && nBurnable.type == Object.TERRAIN && burnTime > 4.0f)
                    {
                        nBurnable.StartFire();
                        burnTime = 0.0f;
                    }

                    if ((nBurnable.State == States.ALIVE && nBurnable.type == Object.ROCK && burnTime > 4.0f) ||
                        (nBurnable.State == States.ALIVE && nBurnable.type == Object.FOLIAGE && burnTime > 2.0f))
                    {
                        nBurnable.Burn();
                    }
                }
            }
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
                    colour.end = Color.black;
                    StartCoroutine(BurnTerrain());
                    break;
                }
            case Object.FOLIAGE:
                {

                    var burns = GetComponentsInChildren<Burnable>();


                    if(burns.Length == 1 && burns[0].name == gameObject.name)
                    {
                        maxScale = gameObject.GetComponent<Transform>().localScale;
                        minScale = maxScale * 0.2f;
                        colour.starts = new Color[gameObject.GetComponent<MeshRenderer>().materials.Length];
                        colour.ends = new Color[colour.starts.Length];
                        for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; ++i)
                        {
                            colour.starts[i] = gameObject.GetComponent<MeshRenderer>().materials[i].color;
                            colour.ends[i] = Color.black;
                        }
                        StartCoroutine(BurnFoliage());
                        return;
                    }

                    foreach(var burn in burns)
                    {
                        burn.Burn();
                    }
                    break;
                }
            case Object.ROCK:
                {
                    colour.start = gameObject.GetComponent<MeshRenderer>().material.color;
                    colour.end = Color.black;
                    StartCoroutine(BurnRock());
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
            burnTime += Time.deltaTime;

            yield return null;
        }


        if(Random.Range(0, 5) != 0)
        {
            while (fadeLife > 0.0f)
            {
                col.a = Mathf.Lerp(0.0f, 1.0f, fadeLife / 100.0f);
                if (col.a < 0.1f)
                    col.a = 0.0f;

                gameObject.GetComponent<MeshRenderer>().material.color = col;

                fadeLife -= Time.deltaTime * fadeRate;

                yield return null;
            }
        }

        state = States.DEAD;
        //FireManager.Instance().unburnedObjectCount--;
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
            burnTime += Time.deltaTime;
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
        //FireManager.Instance().unburnedObjectCount--;
    }

    IEnumerator BurnFoliage()
    {
        Vector3 scale = gameObject.GetComponent<Transform>().localScale;

        while (health >= 0.0f)
        {

            scale = Vector3.Lerp(minScale, maxScale, health / 100.0f);
            gameObject.GetComponent<Transform>().localScale = scale;

            for (int i = 0; i < colour.starts.Length; ++i)
            {
                var col = gameObject.GetComponent<MeshRenderer>().materials[i].color;
                col = Color.Lerp(colour.ends[i], colour.starts[i], (health - 20.0f) / 80.0f);
                gameObject.GetComponent<MeshRenderer>().materials[i].color = col;
            }

            health -= Time.deltaTime * burnRate;
            burnTime += Time.deltaTime;
            yield return null;
        }

        while (fadeLife > 0.0f)
        {
            for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; ++i)
            {
                var col = gameObject.GetComponent<MeshRenderer>().materials[i].color;
                col.a = Mathf.Lerp(0.0f, 1.0f, fadeLife / 100.0f);
                if (col.a < 0.05f)
                    col.a = 0.0f;
                gameObject.GetComponent<MeshRenderer>().materials[i].color = col;
            }

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
            burnTime += Time.deltaTime;
            yield return null;
        }

        state = States.DEAD;
    }

    IEnumerator BurnRock()
    {
        var col = gameObject.GetComponent<MeshRenderer>().material.color;

        while (health >= 0.0f)
        {

            col = Color.Lerp(colour.end, colour.start, health / 100.0f);
            gameObject.GetComponent<MeshRenderer>().material.color = col;

            health -= Time.deltaTime * burnRate;
            burnTime += Time.deltaTime;
            yield return null;
        }

        state = States.DEAD;
    }

    public void StartFire()
    {
        
        if(type != Object.TRUNK && type != Object.LEAVES && type != Object.TERRAIN)
        {
            return;
        }
        fire = Instantiate(ps);
        
        MainSceneManager.instance.BurnCounterIncrease(); //adds 0.05 to the counter once it hits 1, can no longer increase/decrease volume.

        fire.transform.parent = gameObject.transform;
        if (tag == "Terrain")
        {
            var mesh = gameObject.GetComponent<MeshFilter>().mesh;
            var triNum = mesh.triangles.Length;
            var emission = fire.GetComponent<ParticleSystem>().emission;
            emission.rate = new ParticleSystem.MinMaxCurve(2 * triNum);
        }
        var shape = fire.GetComponent<ParticleSystem>().shape;
        shape.shapeType = ParticleSystemShapeType.MeshRenderer;
        shape.meshRenderer = GetComponent<MeshRenderer>();
        shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        Burn();
    }


    IEnumerator EndFire()
    {
        destroyedFire = true;

        if (fire)
        {
            //while (fire.GetComponent<ParticleSystem>().isPlaying)
            //{
            //    fire.GetComponent<ParticleSystem>().Stop(true);
            //    //fire.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            //    yield return null;
            //}

            fire.GetComponent<ParticleSystem>().Stop(true);
            fire.SetActive(false);
        }
        yield return 0;
    }

    void StopFire()
    {
        if (fire)
        {
            fire.GetComponent<ParticleSystem>().Stop(true);

            if (fire.GetComponent<ParticleSystem>().isStopped)
            {
                fire.SetActive(false);
                destroyedFire = true;

                if (type == Object.TERRAIN || type == Object.TRUNK)
                {
                    FireManager.Instance().DecrementUnburnedObject();
                }
            }
        }
        else
        {
            destroyedFire = true;
        }
    }


    public void FindClosestNeighbour()
    {
        neighbours.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(this.transform.position, b.transform.position));
        });


        if(neighbours.Count > 3)
        {
            neighbours.RemoveRange(3, neighbours.Count - 3);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Burnable>())
        {
            if (other.gameObject.GetComponent<Burnable>().type != Object.TERRAIN && 
                type == Object.TERRAIN)
            {
                if(!neighbours.Contains(other.gameObject))
                    neighbours.Add(other.gameObject);
            }
        }
    }

}
