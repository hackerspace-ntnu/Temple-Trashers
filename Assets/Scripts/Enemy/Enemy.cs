using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerBase;

    [ReadOnly]
    public Transform currentTarget;

    public float damageDealt = 1.0f;

    private NavMeshAgent agent;

    public float speed = 5f;

    void Awake()
    {
        if (playerBase == null)
            playerBase = BaseController.Singleton.transform;

        GetComponent<HealthLogic>().onDeath += Die;
    }

    protected void Start()
    {
        currentTarget = playerBase;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void FixedUpdate()
    {
        agent.destination = currentTarget.position;
    }

    void OnDestroy()
    {
        GetComponent<HealthLogic>().onDeath -= Die;
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

    public void Die()
    {
        Destroy(gameObject);
    }
}
