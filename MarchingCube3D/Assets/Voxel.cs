using UnityEngine;
using System;

[Serializable]
public class Voxel
{

    public bool state;

    public Vector3 position, xEdgePosition, yEdgePosition, zEdgePosition;

    public Voxel(int x, int y, int z, float size)
    {
        position.x = (x + 0.5f) * size;
        position.y = (y + 0.5f) * size;
        position.z = (z + 0.5f) * size;

        xEdgePosition = position;
        xEdgePosition.x += size * 0.5f;
        yEdgePosition = position;
        yEdgePosition.y += size * 0.5f;
        zEdgePosition = position;
        zEdgePosition.z += size * 0.5f;
    }
}

