using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(HealthLogic))]
public class SkeletonRagdollController : MonoBehaviour
{
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

    private Rigidbody body;
    private NavMeshAgent agent;
    private HealthLogic health;

    private static readonly int deadAnimatorParam = Animator.StringToHash("Dead");
    private static readonly int deathModeAnimatorParam = Animator.StringToHash("DeathMode");

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Launch;
    }

    void OnDestroy()
    {
        health.onDeath -= Launch;
    }

    private void Launch(DamageInfo dmg)
    {
        anim.SetBool(deadAnimatorParam, true);
        // Set random death animation
        anim.SetFloat(deathModeAnimatorParam, Mathf.Floor(Random.Range(0, 8)));

        agent.enabled = false;
        colliderToDisable.enabled = false;
        colliderToEnable.enabled = true;

        body.centerOfMass = new Vector3(0, 1.5f, 0);
        body.isKinematic = false;
        body.useGravity = true;
        body.maxAngularVelocity = Mathf.Infinity;

        body.AddForce(dmg.KnockBackDir * dmg.KnockBackForce, ForceMode.VelocityChange);
        body.AddTorque(Random.onUnitSphere * Mathf.Pow(Random.Range(0f, 1f), 2) * launchRotationSpeed, ForceMode.VelocityChange);
    }
}
