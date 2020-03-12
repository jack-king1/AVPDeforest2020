using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Burnable : MonoBehaviour
{
    public GameObject fireSound;
     
    public GameObject ps;
    GameObject fire;

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

    public List<GameObject> Neighbours { get => neighbours; set => neighbours = value; }

    float health = 100.0f;

    Vector3 maxScale;
    Vector3 minScale;

    [SerializeField] float burnRate = 1.0f;
    [SerializeField] float fadeRate = 1.0f;
    [SerializeField] float fadeLife = 100.0f;

    bool destroyedFire = false;

    float burnTime = 0.0f;
    private AudioFade fade;

    [SerializeField] private List<GameObject> neighbours = new List<GameObject>();

    private void Awake()
    {
        // fade = GetComponent<AudioFade>();
    }

    // Start is called before the first frame update
    void Update()
    {  
        if (state == States.DEAD && !destroyedFire)
        {      
            StartCoroutine(EndFire());
        }
        else if(state == States.BURN)
        {
            foreach (var n in neighbours)
            {
                var nBurnable = n.GetComponent<Burnable>();
                if (nBurnable)
                {
                    if (nBurnable.type == Object.LEAVES && burnTime > 2.0f)
                    {
                        nBurnable.StartFire();
                    }
                    else if (nBurnable.State == States.ALIVE && nBurnable.type == Object.TRUNK && burnTime > 4.0f)
                    {
                        nBurnable.StartFire();
                    }
                    else if (nBurnable.State == States.ALIVE && nBurnable.type == Object.TERRAIN && burnTime > 4.0f)
                    {
                        nBurnable.StartFire();
                        burnTime = 0.0f;
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

    public void StartFire()
    {
        fire = Instantiate(ps);

        fire.transform.parent = gameObject.transform;
        if (tag == "Terrain")
        {
            var mesh = gameObject.GetComponent<MeshFilter>().mesh;
            var triNum = mesh.triangles.Length;
            var emission = fire.GetComponent<ParticleSystem>().emission;
            emission.rate = new ParticleSystem.MinMaxCurve(3 * triNum);
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
        while (fire.GetComponent<ParticleSystem>().isPlaying)
        {
            fire.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return null;
        }
        FireManager.Instance().RemoveFireSound(fireSound);
        fire.SetActive(false);
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
}
