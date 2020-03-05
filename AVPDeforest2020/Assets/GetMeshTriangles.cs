using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetMeshTriangles : MonoBehaviour
{
    Vector3[] childVertices;
    public Color[] colourMap;
    public Mesh mesh;
    public Material mat;
    public float[] heights;

    private void OnEnable()
    {
        childVertices = GetVerticesInChildren(this.gameObject);
        heights = new float[mesh.vertices.Length];
        GetVertices();
        getMeshHeights();
        normaliseVertices();
        GetColourMap();
        mesh.colors = colourMap;
    }

    Vector3[] GetVerticesInChildren(GameObject go)
    {
        MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter>();
        List<Vector3> vList = new List<Vector3>();
        foreach (MeshFilter mf in mfs)
        {
            vList.AddRange(mf.mesh.vertices);
        }
        return vList.ToArray();
    }

    void normaliseVertices()
    {
        float minHeight = heights.Min();
        float maxHeight = heights.Max();
        for (int i = 0; i < mesh.vertices.Length; ++i)
        {
            float vertexHeight = childVertices[i].y;
            heights[i] = (vertexHeight - minHeight) / (maxHeight - minHeight);
        }
    }

    void GetColourMap()
    {
        colourMap = new Color[mesh.vertices.Length];
        int colourCount = 0;
        for (int j = 0; j < mesh.vertices.Length; ++j)
        {
            for (int i = 0; i < ColourMaps.instance.ForestTerrain.Length; ++i)
            {
                float currentHeight = heights[j];
                for (int k = 0; k < ColourMaps.instance.ForestTerrain.Length; ++k)
                {
                    if (currentHeight <= ColourMaps.instance.ForestTerrain[i].height)
                    {
                        //Debug.Log("Vertex Count: " + colourCount + "Current Vertex Height: " + currentHeight + " Name: " + regions[i].name);
                        colourMap[colourCount] = ColourMaps.instance.DirtTerrain[i].colour;
                    }
                }
            }
            ++colourCount;
        }
    }

    void getMeshHeights()
    {
        heights = new float[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; ++i)
        {
            heights[i] = childVertices[i].y;
        }
    }

    void GetVertices()
    {
        if (mesh)
        {
            childVertices = mesh.vertices;
        }
        else
        {
            Debug.Log("No Mesh Mate!");
        }
    }
}
