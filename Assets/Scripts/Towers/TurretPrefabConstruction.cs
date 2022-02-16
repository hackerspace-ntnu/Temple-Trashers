using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPrefabConstruction : Interactable
{
    public GameObject tower;

    private MeshRenderer[] mrs;
    private SkinnedMeshRenderer[] smrs;

    public Material buildMat;
    public Material cannotBuildMat;

    private void Start()
    {
        mrs = GetComponentsInChildren<MeshRenderer>();
        smrs = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void Construct(HexCell targetCell)
    {
        GameObject t = Instantiate(tower, targetCell.transform.position, tower.transform.rotation);
        targetCell.OccupyingObject = t;
        t.transform.SetParent(targetCell.transform);
        Destroy(gameObject);
    }

    public override void Interact(PlayerStateController player)
    {}

    public void FocusCell(HexCell targetCell)
    {
        transform.position = targetCell.transform.position;

        // Change the material if we cannot build on this cell.
        if (targetCell.IsOccupied)
        {
            foreach(MeshRenderer mr in mrs)
            {
                mr.material = cannotBuildMat;
            }
            foreach(SkinnedMeshRenderer smr in smrs)
            {
                smr.material = cannotBuildMat;
            }
        }
        else
        {
            if(mrs != null)
            {
                foreach (MeshRenderer mr in mrs)
                {
                    mr.material = buildMat;
                }
            }
            
            if(smrs != null)
            {
                foreach (SkinnedMeshRenderer smr in smrs)
                {
                    smr.material = buildMat;
                }
            }
        }
    }
}
