using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform playerBase;

    public Transform currentTarget;

    public float damageDealt = 1.0f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTarget = playerBase;
    }

    void FixedUpdate()
    {
        agent.destination = currentTarget.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        HealthLogic healthComponent = collision.collider.GetComponent<HealthLogic>();
        if (healthComponent)
            healthComponent.DealDamage(damageDealt);
    }

    public void OnPlayerDetected(Transform playerTransform)
    {
        currentTarget = playerTransform;
    }
}
