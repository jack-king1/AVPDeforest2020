using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetMeshTriangles : MonoBehaviour
{
    public Vector3[] childVertices;
    public Color[] colourMap;
    public Material mat;
    public float[] heights;
    public MeshFilter[] terrainMeshFilterRef;

    private void OnEnable()
    {
        GetGameObjecthInChildren();
        childVertices = GetVerticesInChildren(this.gameObject);
        getMeshHeights();
        normaliseVertices();
        GetColourMap();
        AssignTriangleColours();
    }

    void GetGameObjecthInChildren()
    {
        terrainMeshFilterRef = gameObject.GetComponentsInChildren<MeshFilter>();
    }

    Vector3[] GetVerticesInChildren(GameObject go)
    {
        List<Vector3> vList = new List<Vector3>();
        foreach (MeshFilter mf in terrainMeshFilterRef)
        {
            vList.AddRange(mf.mesh.vertices);
        }
        return vList.ToArray();
    }

    void normaliseVertices()
    {
        float minHeight = heights.Min();
        float maxHeight = heights.Max();
        for (int i = 0; i < childVertices.Length; ++i)
        {
            float vertexHeight = childVertices[i].y;
            heights[i] = (vertexHeight - minHeight) / (maxHeight - minHeight);
        }
    }

    void GetColourMap()
    {
        colourMap = new Color[childVertices.Length];
        int colourCount = 0;
        for (int j = 0; j < childVertices.Length; ++j)
        {
            for (int i = 0; i < ColourMaps.instance.ForestTerrain.Length; ++i)
            {
                float currentHeight = heights[j];
                for (int k = 0; k < ColourMaps.instance.ForestTerrain.Length; ++k)
                {
                    if (currentHeight <= ColourMaps.instance.ForestTerrain[i].height)
                    {
                        //Debug.Log("Vertex Count: " + colourCount + "Current Vertex Height: " + currentHeight + " Name: " + regions[i].name);
                        colourMap[colourCount] = ColourMaps.instance.ForestTerrain[i].colour;
                    }
                }
            }
            ++colourCount;
        }
    }

    void getMeshHeights()
    {
        heights = new float[childVertices.Length];
        for (int i = 0; i < childVertices.Length; ++i)
        {
            heights[i] = childVertices[i].y;
        }
    }

    void AssignTriangleColours()
    {
        int verticeCountOffset = 0;
        foreach(MeshFilter mf in terrainMeshFilterRef)
        {
            Color[] meshColour = new Color[mf.mesh.vertices.Length];

            if(verticeCountOffset == 0)
            {
                for (int i = 0; i + verticeCountOffset < verticeCountOffset + mf.mesh.vertices.Length; ++i)
                {
                    meshColour[i] = colourMap[i + verticeCountOffset];
                    ++verticeCountOffset;
                }
            }
            else
            {
                for (int i = 0; i + verticeCountOffset < verticeCountOffset + mf.mesh.vertices.Length; ++i)
                {
                    meshColour[i] = colourMap[i +verticeCountOffset];
                    ++verticeCountOffset;
                }
            }
            mf.mesh.colors = meshColour;
        }
    }
}
