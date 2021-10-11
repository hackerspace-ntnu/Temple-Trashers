using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPrefabConstruction : Interactable
{
    public GameObject tower;

    public void Construct(HexCell targetCell)
    {
        GameObject t = Instantiate(tower, targetCell.transform.position, Quaternion.identity);
        targetCell.SetTower(t);
        Destroy(gameObject);
    }

    public override void Focus(PlayerStateController player)
    {}

    public override void Unfocus(PlayerStateController player)
    {}

    public override void Interact(PlayerStateController player)
    {}

    public void FocusCell(HexCell targetCell)
    {
        transform.position = targetCell.transform.position;
    }
}
