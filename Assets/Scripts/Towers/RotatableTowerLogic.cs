using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableTowerLogic : TowerLogic
{
    public Transform rotAxis;
    private Quaternion initialRotation;

    public GameObject arrowPointer;

    protected new void Start()
    {
        base.Start();

        initialRotation = rotAxis.rotation;

        // Hide arrow pointer initially
        if (arrowPointer)
            arrowPointer.SetActive(false);
    }

    void FixedUpdate()
    {
        ChangeDirection();
    }

    // Rotational movement using aim input
    private void ChangeDirection()
    {
        if (!TurretInput)
            return;

        Vector2 aim = TurretInput.GetAimInput();
        if (aim.sqrMagnitude > 0.01f)
        {
            float angle = -Mathf.Atan2(aim.y, aim.x) * 180f / Mathf.PI; // - 90;
            rotAxis.rotation = Quaternion.Euler(0f, angle, 0f) * initialRotation;
        }
    }

    public override void Focus(PlayerStateController player)
    {
        base.Focus(player);

        if (arrowPointer)
            arrowPointer.SetActive(true);
    }

    public override void Unfocus(PlayerStateController player)
    {
        base.Unfocus(player);

        if (arrowPointer)
            arrowPointer.SetActive(false);
    }
}
