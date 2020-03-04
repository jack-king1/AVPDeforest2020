using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColourMap : MonoBehaviour
{
    public Vector3[] vertices;
    public Color[] colourMap;
    public Mesh mesh;
    public TerrainType[] regions;
    public Material mat;
    public float[] heights;

    void Awake()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = new Vector3[mesh.vertices.Length];
        heights = new float[mesh.vertices.Length];
        GetVertices();
        getMeshHeights();
        normaliseVertices();
        mesh.colors = colourMap;
    }


    private void OnEnable()
    {
        GetColourMap();
    }

    void normaliseVertices()
    {
        float minHeight = heights.Min();
        float maxHeight = heights.Max();
        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            float vertexHeight = vertices[i].y;
            heights[i] = (vertexHeight - minHeight) / (maxHeight - minHeight);
        }
    }

    void GetColourMap()
    {
        colourMap = new Color[mesh.vertices.Length];
        int colourCount = 0;
        for (int j = 0; j < mesh.vertices.Length; ++j)
        {
            for (int i = 0; i < regions.Length; ++i)
            {
                float currentHeight = heights[j];
                for (int k = 0; k < regions.Length; ++k)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        //Debug.Log("Vertex Count: " + colourCount + "Current Vertex Height: " + currentHeight + " Name: " + regions[i].name);
                        colourMap[colourCount] = regions[i].colour;
                    }

                }
            }
            ++colourCount;
            //colourMap[j] = regions[2].colour;
        }
    }

    void getMeshHeights()
    {
        heights = new float[mesh.vertices.Length];
        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            heights[i] = vertices[i].y;
        }
    }

    void GetVertices()
    {
        vertices = mesh.vertices;
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;
    }

}
