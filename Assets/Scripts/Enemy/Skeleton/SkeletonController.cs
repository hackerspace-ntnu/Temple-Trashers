using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform aggroTarget;
    private Transform baseTransform;
    private BaseController baseTarget;
    private HealthLogic health;

    private EnemyState currentState = EnemyState.WALKING;

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
        health.onDeath += (dmg) => SetState(EnemyState.DEAD);
        agent.speed = speed;


        anim.SetFloat("WalkMode", Mathf.Floor(Random.Range(0f, 2f)));
        anim.Play(0, -1, Random.value);
    }


    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.WALKING:
                if((baseTransform.position - transform.position).sqrMagnitude < baseAttackDistance * baseAttackDistance)
                {
                    SetState(EnemyState.ATTACK_BASE);
                }
                break;
            case EnemyState.ATTACK_PLAYER:
                break;
            case EnemyState.ATTACK_BASE:
                break;
            case EnemyState.CHASING:
                if ((aggroTarget.position - transform.position).sqrMagnitude < playerAttackDistance * playerAttackDistance)
                {
                    SetState(EnemyState.ATTACK_PLAYER);
                }
                else if ((aggroTarget.position - transform.position).sqrMagnitude > playerChaseStopDistance * playerChaseStopDistance)
                {
                    SetState(EnemyState.WALKING);
                }
                else
                {
                    agent.destination = aggroTarget.position;
                }
                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }

    }

    private void SetState(EnemyState newState) {

        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
                anim.SetBool("Attack", false);
                break;
            case EnemyState.ATTACK_BASE:
                anim.SetBool("Attack", false);
                break;
            case EnemyState.CHASING:
                anim.SetBool("Chasing", false);
                break;
            case EnemyState.DEAD:
                return;
            default:
                break;
        }

        switch (newState)
        {
            case EnemyState.WALKING:
                agent.destination = baseTransform.position;
                agent.stoppingDistance = baseAttackDistance; ;
                agent.speed = speed;
                aggroTarget = null;
                break;
            case EnemyState.ATTACK_PLAYER:
                anim.SetBool("Attack", true);
                agent.stoppingDistance = playerAttackDistance;
                break;
            case EnemyState.ATTACK_BASE:
                anim.SetBool("Attack", true);
                agent.destination = baseTransform.position;
                agent.stoppingDistance = baseAttackDistance; ;
                break;
            case EnemyState.CHASING:
                agent.speed = chaseSpeed;
                agent.stoppingDistance = playerAttackDistance;
                anim.SetBool("Chasing", true);
                break;
            case EnemyState.DEAD:
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
        if(other.tag == "Player" && currentState == EnemyState.WALKING)
        {
            print("YO");
            aggroTarget = other.transform;
            SetState(EnemyState.CHASING);
        }
    }

    public void DealDamage()
    {
        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
                HealthLogic playerHealth = aggroTarget.GetComponent<HealthLogic>();
                playerHealth.DealDamage(attackDamage);
                if(playerHealth.health <= 0)
                {
                    SetState(EnemyState.WALKING);
                    return;
                }
                if((aggroTarget.position-transform.position).sqrMagnitude > playerAttackDistance * playerAttackDistance)
                {
                    SetState(EnemyState.CHASING);
                }
                break;
            case EnemyState.ATTACK_BASE:
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
            case EnemyState.ATTACK_PLAYER:
                //PLayer he was attacking be dead
                if (aggroTarget)
                {
                    SetState(EnemyState.CHASING);
                }
                else
                {
                    SetState(EnemyState.WALKING);
                }
                break;
            default:
                break;
        }
    }
}
