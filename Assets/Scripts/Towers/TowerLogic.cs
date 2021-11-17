using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : Interactable
{
    private TurretInput _turretInput;

    #region State variables for debugging

    [ReadOnly]
    public TurretInput turretInputReadOnly;

    #endregion State variables for debugging

    protected TurretInput TurretInput { get => _turretInput; private set => turretInputReadOnly = _turretInput = value; }

    protected void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 cellPos = hexGrid.GetCell(transform.position).transform.position;
        transform.position = cellPos;
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        TurretInput = player.GetComponent<TurretInput>();
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        TurretInput = null;
    }

    public override void Interact(PlayerStateController player)
    {}
}
