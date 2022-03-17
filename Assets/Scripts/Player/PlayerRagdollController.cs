using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> bodiesToEnable;

    [SerializeField]
    private List<Rigidbody> bodiesToDisable;

    [SerializeField]
    private List<Collider> collidersToEnable;

    [SerializeField]
    private List<Collider> collidersToDisable;

    [SerializeField]
    private Rigidbody initialForceTarget;

    [SerializeField]
    private float impactForceMultiplier = 1f;

    public void Ragdoll(DamageInfo info)
    {
        foreach (var body in bodiesToEnable)
            body.isKinematic = false;

        foreach (var body in bodiesToDisable)
            body.isKinematic = true;

        foreach (var collider in collidersToDisable)
            collider.enabled = false;

        foreach (var collider in collidersToEnable)
            collider.enabled = true;

        initialForceTarget.AddForce(info.KnockBackDir * info.KnockBackForce * impactForceMultiplier, ForceMode.Impulse);
    }
}
