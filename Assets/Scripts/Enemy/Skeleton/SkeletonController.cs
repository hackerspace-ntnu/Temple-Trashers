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
    private float chaseSpeed = 5f;

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
        baseTransform = baseTarget.transform;
        agent.destination = baseTransform.position;
        aggroTarget = baseTarget.transform;
        health = GetComponent<HealthLogic>();
        health.onDeath += (dmg) => SetState(state.dead);
        agent.speed = speed;


        anim.SetFloat("WalkMode", Mathf.Floor(Random.Range(0f, 2f)));
        anim.Play(0, -1, Random.value);
    }


    private void FixedUpdate()
    {
        switch (currentState)
        {
            case state.walking:
                if((baseTransform.position - transform.position).sqrMagnitude < baseAttackDistance * baseAttackDistance)
                {
                    SetState(state.attackBase);
                }
                break;
            case state.attackPlayer:
                break;
            case state.attackBase:
                break;
            case state.chasing:
                if ((aggroTarget.position - transform.position).sqrMagnitude < playerAttackDistance * playerAttackDistance)
                {
                    SetState(state.attackPlayer);
                }
                else if ((aggroTarget.position - transform.position).sqrMagnitude > playerChaseStopDistance * playerChaseStopDistance)
                {
                    SetState(state.walking);
                }
                else
                {
                    agent.destination = aggroTarget.position;
                }
                break;
            case state.dead:
                break;
            default:
                break;
        }

    }

    private void SetState(state newState) {

        switch (currentState)
        {
            case state.attackPlayer:
                anim.SetBool("Attack", false);
                break;
            case state.attackBase:
                anim.SetBool("Attack", false);
                break;
            case state.chasing:
                anim.SetBool("Chasing", false);
                break;
            case state.dead:
                return;
            default:
                break;
        }

        switch (newState)
        {
            case state.walking:
                agent.destination = baseTransform.position;
                agent.stoppingDistance = baseAttackDistance; ;
                agent.speed = speed;
                aggroTarget = null;
                break;
            case state.attackPlayer:
                anim.SetBool("Attack", true);
                agent.stoppingDistance = playerAttackDistance;
                break;
            case state.attackBase:
                anim.SetBool("Attack", true);
                agent.destination = baseTransform.position;
                agent.stoppingDistance = baseAttackDistance; ;
                break;
            case state.chasing:
                agent.speed = chaseSpeed;
                agent.stoppingDistance = playerAttackDistance;
                anim.SetBool("Chasing", true);
                break;
            case state.dead:
                agent.speed = 0f;
                Destroy(gameObject, 2.5f);
                break;
            default:
                break;
        }
        currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && currentState == state.walking)
        {
            print("YO");
            aggroTarget = other.transform;
            SetState(state.chasing);
        }
    }

    public void DealDamage()
    {
        switch (currentState)
        {
            case state.attackPlayer:
                HealthLogic playerHealth = aggroTarget.GetComponent<HealthLogic>();
                playerHealth.DealDamage(attackDamage);
                if(playerHealth.health <= 0)
                {
                    SetState(state.walking);
                    return;
                }
                if((aggroTarget.position-transform.position).sqrMagnitude > playerAttackDistance * playerAttackDistance)
                {
                    SetState(state.chasing);
                }
                break;
            case state.attackBase:
                baseTransform.GetComponent<HealthLogic>()?.DealDamage(attackDamage);
                break;           
            default:
                break;
        }
    }
    public void Walk()
    {
        switch (currentState)
        {
            case state.attackPlayer:
                //PLayer he was attacking be dead
                if (aggroTarget)
                {
                    SetState(state.chasing);
                }
                else
                {
                    SetState(state.walking);
                }
                break;
            default:
                break;
        }
    }
}
