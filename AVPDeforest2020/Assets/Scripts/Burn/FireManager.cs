using System.Collections;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    static FireManager instance;

    public static FireManager Instance() { return instance; }

    private void Awake()
    {
        instance = this;
        GetBurnables();
    }

    public GameObject psTree;
    public GameObject psTerrain;

    Burnable[] burnables;

    public void StartFire(Collider hit)
    {
        if (hit.gameObject.tag == "Tree" || hit.gameObject.tag == "Terrain")
        {
            if (hit.gameObject.GetComponent<Burnable>())
            {
                SpawnFire(hit.gameObject);
                hit.gameObject.GetComponent<Burnable>().Burn();
            }
        }
    }

    public void StartFire(GameObject burningObject)
    {
         SpawnFire(burningObject);
         burningObject.GetComponent<Burnable>().Burn();
    }

    void SpawnFire(GameObject burningObject)
    {
        if (!burningObject.GetComponent<Burnable>())
            return;

        if (burningObject.GetComponent<Burnable>().State == Burnable.States.ALIVE && !burningObject.GetComponent<Burnable>().ps)
        {
            //var inst = Instantiate(burningObject.tag == "Tree" ? psTree : psTerrain, burningObject.transform);
            var inst = Instantiate(psTerrain, burningObject.transform);

            burningObject.GetComponent<Burnable>().ps = inst;

            var shape = inst.GetComponent<ParticleSystem>().shape;
            var main = inst.GetComponent<ParticleSystem>().main;
            var emission = inst.GetComponent<ParticleSystem>().emission;
            var force = inst.GetComponent<ParticleSystem>().forceOverLifetime;

            force.yMultiplier = 1.0f;

            main.startSize = new ParticleSystem.MinMaxCurve(0.4f, 0.6f);

            if (burningObject.tag == "Terrain")
            {
                force.yMultiplier = 10.0f;
                main.startSize = new ParticleSystem.MinMaxCurve(7f, 9f);
                var mesh = burningObject.GetComponent<MeshFilter>().mesh;
                var triNum = mesh.triangles.Length;
                emission.rate = new ParticleSystem.MinMaxCurve(3 * triNum);
            }

            shape.shapeType = ParticleSystemShapeType.MeshRenderer;
            shape.meshRenderer = burningObject.GetComponent<MeshRenderer>();
            shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        }

    }


    public void GetBurnables()
    {

        StartCoroutine(GetNeighbours());

        //burnables = FindObjectsOfType<Burnable>();


        //for(int i = 0; i < burnables.Length; ++i)
        //{
        //    NodeGrid.Instance().GetObjectIndex(ref burnables[i]);
        //}


        //for (int i = 0; i < burnables.Length; ++i)
        //{
        //    if (burnables[i].type == Burnable.Object.LEAVES)
        //        continue;

        //    for (int j = 0; j < burnables.Length; ++j)
        //    {
        //        if (i == j)
        //            continue;

        //        if (burnables[i].XIndex >= burnables[j].XIndex - 1 && burnables[i].XIndex <= burnables[j].XIndex + 1)
        //        {
        //            if (burnables[i].ZIndex >= burnables[j].ZIndex -1 && burnables[i].ZIndex <= burnables[j].ZIndex + 1)
        //            {
        //                burnables[i].neighbours.Add(burnables[j].gameObject);
        //            }
        //        }
        //    }
        //}

        //for (int i = 0; i < burnables.Length; ++i)
        //{
        //    if (burnables[i].type == Burnable.Object.LEAVES)
        //        continue;

        //    burnables[i].FindClosestNeighbour();
        //}
    }


    IEnumerator GetNeighbours()
    {
        burnables = FindObjectsOfType<Burnable>();

        for (int i = 0; i < burnables.Length; ++i)
        {
            if (burnables[i].type != Burnable.Object.TERRAIN)
                continue;

            for (int j = 0; j < burnables.Length; ++j)
            {
                if (burnables[j].type != Burnable.Object.TERRAIN || i == j)
                    continue;

                if (Connected(burnables[i].gameObject, burnables[j].gameObject))
                    burnables[i].Neighbours.Add(burnables[j].gameObject);
            }
        }


        yield return 0;
    }

    bool Connected(GameObject a, GameObject b)
    {
        int connectedAmount = 0;
        var aVerts = a.GetComponent<MeshFilter>().mesh.vertices;
        var bVerts = b.GetComponent<MeshFilter>().mesh.vertices;
        for (int i  = 0; i < aVerts.Length; ++i)
        {
            for (int j = 0; j < bVerts.Length; ++j)
            {

                if (aVerts[i] == bVerts[j])
                    connectedAmount++;

                if (connectedAmount >= 2)
                    return true;

            }
        }

        return false;
    }

}
