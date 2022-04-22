using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(HealthLogic))]
public class EnemyLightRagdollController : MonoBehaviour
{
    [SerializeField]
    protected Collider colliderToDisable = default;

    [SerializeField]
    protected Collider colliderToEnable = default;

    [SerializeField]
    protected float launchRotationSpeed = default;

    protected Rigidbody body;
    protected NavMeshAgent agent;
    protected Animator animator;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Launch(DamageInfo dmg)
    {
        SetAnimatorParams();

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

    protected virtual void SetAnimatorParams()
    {}

    protected virtual Vector3 GetLaunchTorque()
    {
        float randomLaunchRotationSpeed = Mathf.Pow(Random.value, 2) * launchRotationSpeed;
        return randomLaunchRotationSpeed * Random.onUnitSphere;
    }
}
