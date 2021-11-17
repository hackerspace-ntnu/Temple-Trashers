using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(HealthLogic))]
public class SkeletonRagdollController : MonoBehaviour
{
    private Rigidbody body;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Collider colliderToDisable;

    [SerializeField]
    private Collider colliderToEnable;

    [SerializeField]
    private Vector2 launchSpeed;

    [SerializeField]
    private float launchRotationSpeed;

    private void Awake()
    {
        GetComponent<HealthLogic>().onDeath += Launch;
    }

    private void Launch(HealthLogic.DamageInstance dmg)
    {
        anim.SetBool("Dead", true);
        anim.SetFloat("DeathMode", Mathf.Floor(Random.Range(0, 8)));
        GetComponent<NavMeshAgent>().enabled = false;

        colliderToDisable.enabled = false;
        colliderToEnable.enabled = true;

        body = GetComponent<Rigidbody>();
        body.centerOfMass = new Vector3(0, 1.5f, 0);
        body.isKinematic = false;
        body.useGravity = true;
        body.maxAngularVelocity = Mathf.Infinity;

        body.AddForce( dmg.KnockBackDir*dmg.KnockBackForce, ForceMode.VelocityChange);
        body.AddTorque(Random.onUnitSphere * Mathf.Pow(Random.Range(0f,1f),2) * launchRotationSpeed, ForceMode.VelocityChange);
    }
}
