using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerLogic : Interactable
{
    [SerializeField]
    private TowerScriptableObject _towerScriptableObject;

    [ReadOnly, SerializeField]
    protected TurretInput turretInput;

    public TowerScriptableObject TowerScriptableObject => _towerScriptableObject;

    private RepairAnimationController repairAnimationController;

    void Awake()
    {
        repairAnimationController = GetComponent<RepairAnimationController>();
    }

    protected void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 cellPos = hexGrid.GetCell(transform.position).transform.position;
        transform.position = cellPos;
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        turretInput = player.GetComponent<TurretInput>();
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        turretInput = null;
    }

    public override void Interact(PlayerStateController player)
    {
        Repair();
    }

    private void Repair()
    {
        if (repairAnimationController)
            repairAnimationController.Repair();
    }
}
