using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableTowerLogic : TowerLogic
{
    public Transform rotAxis;
    private Quaternion initialRotation;

    public GameObject arrowPointer;
    private Renderer arrowPointerRenderer;

    protected new void Start()
    {
        base.Start();

        initialRotation = rotAxis.rotation;
        // Some towers might not use an arrow pointer
        arrowPointerRenderer = arrowPointer ? arrowPointer.GetComponent<Renderer>() : null;
    }

    void FixedUpdate()
    {
        ChangeDirection();
    }

    // Rotational movement using aim input
    private void ChangeDirection()
    {
        if (!input)
            return;

        Vector2 aim = input.GetAimInput();
        if (aim.sqrMagnitude > 0.01f)
        {
            float angle = -Mathf.Atan2(aim.y, aim.x) * 180f / Mathf.PI; // - 90;
            rotAxis.rotation = Quaternion.Euler(0f, angle, 0f) * initialRotation;
        }
    }

    public override void Focus(PlayerStateController player)
    {
        base.Focus(player);

        // render arrowPointer
        if (arrowPointerRenderer)
            arrowPointerRenderer.enabled = true;
    }

    public override void Unfocus(PlayerStateController player)
    {
        base.Unfocus(player);

        // unrender pointer
        if (arrowPointerRenderer)
            arrowPointerRenderer.enabled = false;
    }
}
