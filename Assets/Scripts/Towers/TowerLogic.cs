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

    [SerializeField]
    private TutorialText tutorialText;

    void Awake()
    {
        repairAnimationController = GetComponent<RepairAnimationController>();
    }

    protected void Start()
    {
        Vector3 cellPos = HexGrid.Singleton.GetCell(transform.position).transform.position;
        transform.position = cellPos;

        // Setup tutorial ui elements           
        tutorialText.SetButton(TutorialText.Direction.North, true);
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
        if(tutorialText)
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
