using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColourMap : MonoBehaviour
{
    public Vector3[] vertices;
    public Color[] colourMap;
    public Mesh mesh;
    public Material mat;
    public float[] heights;
    public bool dirt, forest;

    void Awake()
    {

    }

    private void OnEnable()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = new Vector3[mesh.vertices.Length];
        heights = new float[mesh.vertices.Length];
        GetVertices();
        getMeshHeights();
        normaliseVertices();
        GetColourMap();
        mesh.colors = colourMap;
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
        if (dirt)
        {
            for (int j = 0; j < mesh.vertices.Length; ++j)
            {
                for (int i = 0; i < ColourMaps.instance.DirtTerrain.Length; ++i)
                {
                    float currentHeight = heights[j];
                    for (int k = 0; k < ColourMaps.instance.DirtTerrain.Length; ++k)
                    {
                        if (currentHeight <= ColourMaps.instance.DirtTerrain[i].height)
                        {
                            colourMap[colourCount] = ColourMaps.instance.DirtTerrain[i].colour;
                        }
                    }
                }
                ++colourCount;
            }
        }
        else if (forest)
        {
            for (int j = 0; j < mesh.vertices.Length; ++j)
            {
                for (int i = 0; i < ColourMaps.instance.ForestTerrain.Length; ++i)
                {
                    float currentHeight = heights[j];
                    for (int k = 0; k < ColourMaps.instance.ForestTerrain.Length; ++k)
                    {
                        if (currentHeight <= ColourMaps.instance.ForestTerrain[i].height)
                        {
                            colourMap[colourCount] = ColourMaps.instance.ForestTerrain[i].colour;


                        }
                    }
                }
                ++colourCount;
            }
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
        if(mesh)
        {
            vertices = mesh.vertices;
        }
        else
        {
            Debug.Log("No Mesh Mate!");
        }

    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;
    }
}
