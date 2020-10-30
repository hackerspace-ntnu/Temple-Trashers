using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform playerBase;

    public float damageDealt = 1.0f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        agent.destination = playerBase.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        HealthLogic healthComponent = collision.collider.GetComponent<HealthLogic>();
        if (healthComponent)
            healthComponent.DealDamage(damageDealt);
    }
}
