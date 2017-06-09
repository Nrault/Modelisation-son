using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube {

    public Voxel[] voxels = new Voxel[8];
    public Vector3[] edges = new Vector3[12];
    public Vector3 pos;

    public Cube()
    {
        for (int i = 0; i < voxels.Length; i++)
        {
            voxels[i] = null;
        }
    }
}
