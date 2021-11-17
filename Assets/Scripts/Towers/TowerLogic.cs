using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : Interactable
{
    public TurretInput input;

    protected void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 cellPos = hexGrid.GetCell(transform.position).transform.position;
        transform.position = cellPos;
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        input = player.GetComponent<TurretInput>();
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        input = null;
    }

    public override void Interact(PlayerStateController player)
    {}
}
