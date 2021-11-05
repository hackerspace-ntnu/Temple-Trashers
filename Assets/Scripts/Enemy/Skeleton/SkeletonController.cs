using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    private enum state
    {
        walking,
        attackPlayer,
        attackBase,
        chasing,
        dead
    }
    private NavMeshAgent agent;
    private Transform aggroTarget;
    private Transform baseTransform;
    private BaseController baseTarget;
    private HealthLogic health;

    private state currentState = state.walking;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float baseAttackDistance = 2f;

    [SerializeField]
    private float playerAttackDistance = 1f;

    [SerializeField]
    private float playerChaseStopDistance = 4f;


    [SerializeField]
    private float attackDamage = 1f;

    private const float THRESHOLD = 0.001f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        baseTarget = BaseController.Singleton;
        aggroTarget = baseTarget.transform;
        health = GetComponent<HealthLogic>();
        health.onDeath += () => SetState(state.dead);
        agent.speed = speed;

        anim.Play(0, -1, Random.value);
    }


    private void FixedUpdate()
    {
        switch (currentState)
        {
            case state.walking:
                if((baseTransform.position - transform.position).sqrMagnitude < baseAttackDistance * baseAttackDistance)
                {
                    anim.SetBool("Attack", true);
                    currentState = state.attackBase;
                    agent.speed = 0f;
                }
                break;
            case state.attackPlayer:

                break;
            case state.attackBase:
                break;
            case state.chasing:
                break;
            case state.dead:
                break;
            default:
                break;
        }

    }

    private void SetState(state newState) {
        if (currentState == state.dead) return;

        switch (newState)
        {
            case state.walking:
                agent.destination = baseTransform.position;
                agent.speed = speed;
                break;
            case state.attackPlayer:
                anim.SetBool("Attack", true);
                agent.speed = 0;
                break;
            case state.attackBase:
                anim.SetBool("Attack", true);
                agent.destination = baseTransform.position;
                agent.speed = 0f;
                break;
            case state.chasing:
                agent.speed = speed;
                break;
            case state.dead:
                agent.speed = 0f;
                Destroy(gameObject, 2.5f);
                break;
            default:
                break;
        }
    }

    

    public void DealDamage()
    {
        aggroTarget.GetComponent<HealthLogic>()?.DealDamage(attackDamage);
    }
    public void Walk()
    {
        SetState(state.walking);
    }
}
