using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnigolemRagdollController : EnemyLightRagdollController
{
    protected override Vector3 GetLaunchTorque()
    {
        // Using `forward` here would have made the most sense to me
        // (getting the body to rotate in the same direction as this collider is facing),
        // but that would have caused the body to rotate in its left-facing direction
        return agent.speed * colliderToEnable.transform.right;
    }
}
