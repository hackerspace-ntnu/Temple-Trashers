using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class RotatableTowerLogic : TowerLogic
{
    [SerializeField, Tooltip("This object should be rotated so that it's facing the direction of the red (X) axis.")]
    private Transform rotationAxis = default;

    [SerializeField]
    private GameObject directionalPointer = default;

    [SerializeField]
    private int rotationDelay = 200;

    private Quaternion initialRotation;
    private int lastTweenId = -1;

    public Transform RotationAxis => rotationAxis;

    protected new void Awake()
    {
        base.Awake();

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
            // Assumes that the initial rotation has been set so that it faces the direction of the red (X) axis (aka. `right`)
            Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, new Vector3(aim.x, 0f, aim.y));
            rotationAxis.rotation = Quaternion.RotateTowards(rotationAxis.rotation, fromToRotation * initialRotation, Time.fixedDeltaTime * rotationDelay);
        }
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
