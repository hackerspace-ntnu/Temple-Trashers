using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonController : Enemy
{
    private static readonly int walkModeAnimatorParam = Animator.StringToHash("WalkMode");

    protected override void AnimationSetup()
    {
        // Set random walk animation
        anim.SetFloat(walkModeAnimatorParam, Mathf.Floor(Random.Range(0, 2)));
    }
}
