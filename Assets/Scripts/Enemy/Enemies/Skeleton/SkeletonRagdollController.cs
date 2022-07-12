using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonRagdollController : EnemyLightRagdollController
{
    private static readonly int deadAnimatorParam = Animator.StringToHash("Dead");
    private static readonly int deathModeAnimatorParam = Animator.StringToHash("DeathMode");

    protected override void SetAnimatorParams()
    {
        base.SetAnimatorParams();

        animator.SetBool(deadAnimatorParam, true);
        // Set random death animation
        animator.SetFloat(deathModeAnimatorParam, Random.Range(0, 8));
    }
}
