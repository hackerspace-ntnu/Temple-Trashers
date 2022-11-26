using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> bodiesToEnable = default;

    [SerializeField]
    private List<Rigidbody> bodiesToDisable = default;

    [SerializeField]
    private List<Collider> collidersToEnable = default;

    [SerializeField]
    private List<Collider> collidersToDisable = default;

    public Rigidbody initialForceTarget = default;

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

    public void UnRagdoll()
    {
        foreach (var body in GetComponentsInChildren<Rigidbody>())
        {
            body.isKinematic = true;
            body.velocity = Vector3.zero; body.angularVelocity = Vector3.zero;
        }
            
    }
}
