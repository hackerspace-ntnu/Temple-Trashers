using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class RotatableTowerLogic : TowerLogic
{
    [SerializeField]
    private GameObject directionalPointer = default;

    [SerializeField]
    private int rotationDelay = 200;

    private int lastTweenId = -1;

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
            RotateFacing(new Vector3(aim.x, 0f, aim.y), rotationDelay);
    }

    public override void Focus(PlayerStateController player)
    {
        base.Focus(player);

        if (directionalPointer)
        {
            ScaleDirectionalPointer(Vector3.one);
            directionalPointer.GetComponent<VisualEffect>().SetVector4("Color", player.FocusedColor);
        }
    }

    public override void Unfocus(PlayerStateController player)
    {
        base.Unfocus(player);

        if (directionalPointer)
            ScaleDirectionalPointer(Vector3.zero);
    }

    private void ScaleDirectionalPointer(Vector3 toScale)
    {
        if (lastTweenId != -1)
            LeanTween.cancel(lastTweenId);

        lastTweenId = LeanTween.scale(directionalPointer, toScale, 0.15f).setEaseInOutQuad().id;
    }
}
