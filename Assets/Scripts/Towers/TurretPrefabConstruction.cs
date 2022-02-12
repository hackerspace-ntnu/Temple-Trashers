using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPrefabConstruction : Interactable
{
    public GameObject tower;

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
    }
}
