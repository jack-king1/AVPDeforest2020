using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMaps : MonoBehaviour
{
    public static ColourMaps instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;
    }

    public TerrainType[] DirtTerrain;
    public TerrainType[] ForestTerrain;
}
