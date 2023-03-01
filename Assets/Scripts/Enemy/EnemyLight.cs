using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Abstraction class for light enemies, like the skeleton and unigolem
/// (as opposed to heavy enemies, like the golem).
/// </summary>
public abstract class EnemyLight : Enemy
{
    [SerializeField]
    protected float durationBeforeDespawn = 2.5f;

    //protected EnemyLightRagdollController ragdollController;

    [Header("Ragdoll related fields")]
    [SerializeField]
    protected Collider colliderToDisable = default;

    [SerializeField]
    protected Collider colliderToEnable = default;

    [SerializeField]
    protected float launchRotationSpeed = default;

    private Rigidbody body;


    protected override void Awake()
    {
        base.Awake();
        body = GetComponent<Rigidbody>();
    }

    protected override void Die(DamageInfo damageInfo)
    {
        base.Die(damageInfo);

        // Ensure that the enemy is launched *after* the `SetState()` logic is executed, to not mess up things like the navmesh agent
        Launch(damageInfo);
    }

    // Ragdoll methods
    public void Launch(DamageInfo dmg)
    {
        SetRagdollAnimatorParams();

        agent.enabled = false;
        colliderToDisable.enabled = false;
        colliderToEnable.enabled = true;

        // Assumes that the collider is placed at (0, 0, 0) relative to the Rigidbody gameobject
        body.centerOfMass = transform.InverseTransformPoint(colliderToEnable.bounds.center);
        body.isKinematic = false;
        body.useGravity = true;
        body.maxAngularVelocity = Mathf.Infinity;

        body.AddForce(dmg.KnockBackDir * dmg.KnockBackForce, ForceMode.VelocityChange);
        body.AddTorque(GetLaunchTorque(), ForceMode.VelocityChange);
    }

    protected virtual void SetRagdollAnimatorParams()
    { }

    protected virtual Vector3 GetLaunchTorque()
    {
        float randomLaunchRotationSpeed = Mathf.Pow(Random.value, 2) * launchRotationSpeed;
        return randomLaunchRotationSpeed * Random.onUnitSphere;
    }
}
