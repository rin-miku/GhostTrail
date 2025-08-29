using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public LayerMask meshLayerMask;
    public float ghostLifeTime;
    public List<Material> ghostMaterials;

    private List<Ghost> ghosts;

    private void Start()
    {
        ghosts = new List<Ghost>();
    }

    private void Update()
    {
        float time = Time.time;
        for (int i = ghosts.Count-1; i >= 0; i--)
        {
            Ghost ghost = ghosts[i];
            if (time - ghost.createTime > ghostLifeTime)
            {
                ghost.RemoveGhost();
                ghosts.RemoveAt(i);
            }
            else
            {
                ghost.UpdateGhost();
            }
        }
    }

    public void CreateGhost()
    {
        Ghost ghost = GhostPool.Get();

        ghost.createTime = Time.time;
        ghost.lifeTime = ghostLifeTime;
        ghost.mesh.Clear();
        meshRenderer.BakeMesh(ghost.mesh);
        ghost.matrix = Matrix4x4.TRS(meshRenderer.transform.position, meshRenderer.transform.rotation, transform.transform.localScale);
        ghost.layer = meshLayerMask.value;
        ghost.materials = ghostMaterials;

        ghosts.Add(ghost);
    }
}
