using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMap : MonoBehaviour
{
    public Vector3[] vertices;
    public Vector3[] normalisedVertices;
    public Color[] colourMap;
    public Mesh mesh;
    public TerrainType[] regions;
    public Material mat;
    public static ColourMap instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = new Vector3[mesh.vertices.Length];
        normalisedVertices = new Vector3[mesh.vertices.Length];
        GetVertices();
        normaliseVertices();
        GetColourMap();
        mesh.colors = colourMap;
    }

    void normaliseVertices()
    {
        for (int i = 0; i < vertices.Length; ++i)
        {
            normalisedVertices[i] = Vector3.Normalize(vertices[i]);
        }  
    }

    void GetColourMap()
    {
        colourMap = new Color[mesh.vertices.Length];
        int colourCount = 0;   
        for(int j = 0; j < mesh.vertices.Length; ++j)
        {
            //for (int i = 0; i < regions.Length; ++i)
            //{
            //    float currentHeight = normalisedVertices[j].y;
            //    if (currentHeight < regions[i].height)
            //    {    
            //        colourMap[colourCount] = regions[i].colour;
            //    }
            //    ++colourCount;
            //}

            colourMap[j] = regions[2].colour;
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
