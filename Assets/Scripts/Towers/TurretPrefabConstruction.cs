using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretPrefabConstruction : Interactable
{
    public GameObject tower;

    [SerializeField]
    private TowerScriptableObject _towerScriptableObject;

    public TowerScriptableObject TowerScriptableObject => _towerScriptableObject;

    public void Construct(HexCell targetCell)
    {
        GameObject t = Instantiate(tower, targetCell.transform.position, tower.transform.rotation, targetCell.transform);
        targetCell.OccupyingObject = t;
        Destroy(gameObject);
    }
    public override void Interact(PlayerStateController player)
    { }
    public void FocusCell(HexCell targetCell)
    {
        transform.position = targetCell.transform.position;
    }
}