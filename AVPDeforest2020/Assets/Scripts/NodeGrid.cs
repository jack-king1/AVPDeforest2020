using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{

    static NodeGrid instance;

    public static NodeGrid Instance() { return instance; }


    public int nodeCount = 20;

    class Bounds2D
    {
        Vector3 center;
        Vector3 min;
        Vector3 max;
        int xIndex;
        int zIndex;

        public Vector3 Center { get => center; }
        public Vector3 Min { get => min; }
        public Vector3 Max { get => max; }

        public Bounds2D(Vector3 pos, int nodeCount, int _xIndex, int _zIndex)
        {
            center = pos;
            min = center - new Vector3((300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f);
            max = center + new Vector3((300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f);
            xIndex = _xIndex;
            zIndex = _zIndex;

        }

        public void SetCenter(Vector3 pos, int nodeCount)
        {
            center = pos;
            min = center - new Vector3((300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f);
            max = center + new Vector3((300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f, (300.0f / nodeCount) / 2.0f);
        }


        public bool IsInside(Vector3 pos)
        {
            return ((pos.x <= max.x && pos.x >= min.x) && (pos.z <= max.z && pos.z >= min.z));
        }



    }


    Bounds2D[,] nodeGrid;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        nodeGrid = new Bounds2D[nodeCount, nodeCount];

        int xCenter = 300 / nodeCount;
        int zCenter = 300 / nodeCount;

        for (int i  = 0; i < nodeCount; ++i)
        {
            for (int j = 0; j < nodeCount; ++j)
            {
                nodeGrid[i, j] = new Bounds2D(new Vector3(xCenter, 0, zCenter), nodeCount, i, j);
                zCenter += 300 / nodeCount;
            }
            xCenter += 300 / nodeCount;
            zCenter = 300 / nodeCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetObjectIndex(ref Burnable burnable)
    {
        for (int i = 0; i < nodeCount; ++i)
        {
            for (int j = 0; j < nodeCount; ++j)
            {
                if(nodeGrid[i, j].IsInside(burnable.transform.position))
                {
                    burnable.XIndex = i;
                    burnable.ZIndex = j;
                }
            }
        }
    }


}
