using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractTower : Interactable
{
    [SerializeField]
    protected TowerScriptableObject _towerScriptableObject = default;

    [SerializeField, Tooltip("This object should be rotated so that it's facing the direction of the red (X) axis.")]
    protected Transform rotationAxis = default;

    protected Quaternion initialRotation;

    public TowerScriptableObject TowerScriptableObject => _towerScriptableObject;

    public Transform RotationAxis => rotationAxis;

    protected void Awake()
    {
        initialRotation = rotationAxis.rotation;
    }

    /// <summary>
    /// Rotates the tower along the XZ plane (i.e. around the Y axis) so that it's facing <c>facingDir</c>.
    /// </summary>
    /// <param name="facingDir"></param>
    /// <param name="animationDuration">
    ///     Setting this to 0 (default) makes the rotation happens instantaneously.
    ///     Setting it to anything greater than 0, makes the rotation stepwise, and this method should in that case be called every `FixedUpdate()`
    ///     to allow the rotation "animation" to finish.
    /// </param>
    public void RotateFacing(Vector3 facingDir, float animationDuration = 0f)
    {
        // Ensure that the tower only has its yaw changed (rotated around the Y axis)
        facingDir.y = 0f;
        // Assumes that the initial rotation has been set so that it faces the direction of the red (X) axis (aka. `right`)
        Quaternion newRotation = Quaternion.FromToRotation(Vector3.right, facingDir) * initialRotation;
        if (animationDuration > 0f)
            rotationAxis.rotation = Quaternion.RotateTowards(rotationAxis.rotation, newRotation, Time.fixedDeltaTime * animationDuration);
        else
            rotationAxis.rotation = newRotation;
    }
}
