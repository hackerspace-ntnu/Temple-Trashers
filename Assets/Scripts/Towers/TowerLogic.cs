using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerLogic : AbstractTower
{
    [ReadOnly, SerializeField]
    protected TurretInput turretInput;

    [SerializeField]
    private TutorialText tutorialText = default;

    private RepairAnimationController repairAnimationController;

    new void Awake()
    {
        base.Awake();

        repairAnimationController = GetComponent<RepairAnimationController>();
    }

    void Start()
    {
        Vector3 cellPos = HexGrid.Singleton.GetCell(transform.position).transform.position;
        transform.position = cellPos;

        // Setup tutorial ui elements           
        tutorialText.SetButton(Direction.NORTH, true);
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        turretInput = player.GetComponent<TurretInput>();

        if (tutorialText)
            tutorialText.Focus();
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        turretInput = null;

        if (tutorialText)
            tutorialText.Unfocus();
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
