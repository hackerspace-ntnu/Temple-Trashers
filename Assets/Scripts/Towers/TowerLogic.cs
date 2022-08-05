using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TowerLogic : AbstractTower
{
    [SerializeField]
    private GameObject directionalPointer = default;

    [SerializeField]
    private float rotationDuration = 200f;

    [ReadOnly, SerializeField]
    protected TurretInput turretInput;

    [SerializeField]
    private TutorialText tutorialText = default;

    private RepairAnimationController repairAnimationController;
    private int lastTweenId = -1;

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

    void FixedUpdate()
    {
        ChangeDirection();
    }

    // Rotational movement using aim input
    private void ChangeDirection()
    {
        if (!turretInput)
            return;

        Vector2 aim = turretInput.GetAimInput();
        if (aim.sqrMagnitude > 0.01f)
            RotateFacing(new Vector3(aim.x, 0f, aim.y), rotationDuration);
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        turretInput = player.GetComponent<TurretInput>();

        if (directionalPointer)
        {
            ScaleDirectionalPointer(Vector3.one);
            directionalPointer.GetComponent<VisualEffect>().SetVector4("Color", player.FocusedColor);
        }

        if (tutorialText)
            tutorialText.Focus();
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        turretInput = null;

        if (directionalPointer)
            ScaleDirectionalPointer(Vector3.zero);

        if (tutorialText)
            tutorialText.Unfocus();
    }

    private void ScaleDirectionalPointer(Vector3 toScale)
    {
        if (lastTweenId != -1)
            LeanTween.cancel(lastTweenId);

        lastTweenId = LeanTween.scale(directionalPointer, toScale, 0.15f).setEaseInOutQuad().id;
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
