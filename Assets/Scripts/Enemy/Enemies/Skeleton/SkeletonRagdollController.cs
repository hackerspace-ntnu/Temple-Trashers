using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(HealthLogic))]
public class SkeletonRagdollController : MonoBehaviour
{
    [SerializeField]
    private Collider colliderToDisable = default;

    [SerializeField]
    private Collider colliderToEnable = default;

    [SerializeField]
    private float launchRotationSpeed = default;

    private Rigidbody body;
    private NavMeshAgent agent;
    private HealthLogic health;
    private Animator animator;

    private static readonly int deadAnimatorParam = Animator.StringToHash("Dead");
    private static readonly int deathModeAnimatorParam = Animator.StringToHash("DeathMode");

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Launch;
        animator = GetComponentInChildren<Animator>();
    }

    void OnDestroy()
    {
        health.onDeath -= Launch;
    }

    private void Launch(DamageInfo dmg)
    {
        animator.SetBool(deadAnimatorParam, true);
        // Set random death animation
        animator.SetFloat(deathModeAnimatorParam, Random.Range(0, 8));

        agent.enabled = false;
        colliderToDisable.enabled = false;
        colliderToEnable.enabled = true;

        body.centerOfMass = new Vector3(0, 1.5f, 0);
        body.isKinematic = false;
        body.useGravity = true;
        body.maxAngularVelocity = Mathf.Infinity;

        body.AddForce(dmg.KnockBackDir * dmg.KnockBackForce, ForceMode.VelocityChange);
        float randomLaunchRotationSpeed = Mathf.Pow(Random.value, 2) * launchRotationSpeed;
        body.AddTorque(randomLaunchRotationSpeed * Random.onUnitSphere, ForceMode.VelocityChange);
    }
}
