using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RotatableTowerLogic : TowerLogic
{
    [SerializeField]
    private Transform rotationAxis = default;

    [SerializeField]
    private GameObject directionalPointer = default;

    [SerializeField]
    private int rotationDelay = 200;

    private Quaternion initialRotation;
    private int lastTweenId = -1;

    public Transform RotationAxis => rotationAxis;

    protected new void Start()
    {
        base.Start();

        initialRotation = rotationAxis.rotation;
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
        {
            float angle = -Mathf.Atan2(aim.y, aim.x) * 180f / Mathf.PI;
            rotationAxis.rotation = Quaternion.RotateTowards(rotationAxis.rotation, Quaternion.Euler(0f, angle, 0f)*initialRotation, Time.deltaTime*rotationDelay);
        }
    }

    public override void Focus(PlayerStateController player)
    {
        base.Focus(player);

        if (directionalPointer)
        {
            if (lastTweenId != -1)
                LeanTween.cancel(lastTweenId);

            lastTweenId = LeanTween.scale(directionalPointer, Vector3.one, 0.15f).setEaseInOutQuad().id;
            directionalPointer.GetComponent<VisualEffect>().SetVector4("Color", player.FocusedColor);
        }
    }

    public override void Unfocus(PlayerStateController player)
    {
        base.Unfocus(player);

        if (directionalPointer)
        {
            if (lastTweenId != -1)
            {
                LeanTween.cancel(lastTweenId);
            }

            lastTweenId = LeanTween.scale(directionalPointer, Vector3.zero, 0.15f).setEaseInOutQuad().id;
        }
    }
}
