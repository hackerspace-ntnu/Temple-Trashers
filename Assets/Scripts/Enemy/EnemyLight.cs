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

    protected EnemyLightRagdollController ragdollController;

    protected override void Awake()
    {
        base.Awake();

        ragdollController = GetComponent<EnemyLightRagdollController>();
    }

    protected override void Die(DamageInfo damageInfo)
    {
        base.Die(damageInfo);

        // Ensure that the enemy is launched *after* the `SetState()` logic is executed, to not mess up things like the navmesh agent
        ragdollController.Launch(damageInfo);
    }
}
