using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPrefabConstruction : Interactable
{
    [SerializeField]
    private GameObject towerPrefab;

    public void Construct(HexCell targetCell)
    {
        GameObject tower = Instantiate(towerPrefab, targetCell.transform.position, towerPrefab.transform.rotation, targetCell.transform);
        targetCell.OccupyingObject = tower;
        Destroy(gameObject);
    }

    public override void Interact(PlayerStateController player)
    {}

    public void FocusCell(HexCell targetCell)
    {
        transform.position = targetCell.transform.position;
    }
}
