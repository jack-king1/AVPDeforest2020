using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    static FireManager instance;

    public static FireManager Instance() { return instance; }

    private void Start()
    {
        instance = this;
    }

    public GameObject psTree;
    public GameObject psTerrain;

    public void SpawnFire(Collider hit)
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

    void SpawnFire(GameObject burningObject)
    {
        if (burningObject.GetComponent<Burnable>())
        {
            if (burningObject.GetComponent<Burnable>().State == Burnable.States.ALIVE && !burningObject.GetComponent<Burnable>().ps)
            {
                var inst = Instantiate(burningObject.tag == "Tree" ? psTree : psTerrain, burningObject.transform);

                burningObject.GetComponent<Burnable>().ps = inst;

                var shape = inst.GetComponent<ParticleSystem>().shape;

                var main = inst.GetComponent<ParticleSystem>().main;
                var startSize = main.startSize;
                var emission = inst.GetComponent<ParticleSystem>().emission;
                var rate = emission.rateOverTime;

                if (burningObject.tag == "Terrain")
                {
                    main.startSize = new ParticleSystem.MinMaxCurve(10f, 12f);
                    var mesh = burningObject.GetComponent<MeshFilter>().mesh;
                    var triNum = mesh.triangles.Length;
                    emission.rate = new ParticleSystem.MinMaxCurve(5 * triNum);
                }

                shape.shapeType = ParticleSystemShapeType.MeshRenderer;
                shape.meshRenderer = burningObject.GetComponent<MeshRenderer>();
                shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
            }
        }

    }
}
