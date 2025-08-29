using System.Collections.Generic;
using UnityEngine;

public class Ghost
{
    public float createTime;
    public float lifeTime;

    public Mesh mesh;
    public Matrix4x4 matrix;
    public int layer;
    public List<Material> materials;

    private MaterialPropertyBlock materialPropertyBlock;

    public Ghost()
    {
        mesh = new Mesh();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void UpdateGhost()
    {
        if(materials.Count < mesh.subMeshCount)
        {
            Debug.LogError("Check your subMesh materials!");
            return;
        }

        float alpha = 1 - Mathf.Clamp01((Time.time - createTime) / lifeTime);
        materialPropertyBlock.SetFloat("_Alpha", alpha);

        for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
        {
            Graphics.DrawMesh(mesh, matrix, materials[subMeshIndex], layer, null, subMeshIndex, materialPropertyBlock, false, false, false);
        }
    }

    public void RemoveGhost()
    {
        mesh.Clear();
        GhostPool.Return(this);
    }
}
