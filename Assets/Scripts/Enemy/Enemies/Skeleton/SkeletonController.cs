using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    [ReadOnly, SerializeField]
    private Transform aggroTarget;

    [ReadOnly, SerializeField]
    private Transform baseTransform;

    [ReadOnly, SerializeField]
    private BaseController baseTarget;

    private NavMeshAgent agent;
    private HealthLogic health;

    [ReadOnly, SerializeField]
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

    private static readonly int walkModeAnimatorParam = Animator.StringToHash("WalkMode");
    private static readonly int attackAnimatorParam = Animator.StringToHash("Attack");
    private static readonly int chasingAnimatorParam = Animator.StringToHash("Chasing");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        baseTarget = BaseController.Singleton;
        baseTransform = baseTarget.transform;
        agent.destination = baseTransform.position;
        aggroTarget = baseTransform;

        health = GetComponent<HealthLogic>();
        health.onDeath += Die;

        anim.SetFloat(walkModeAnimatorParam, Mathf.Floor(Random.Range(0f, 2f)));
        anim.Play(0, -1, Random.value);
    }

    private void Die(DamageInfo dmg)
    {
        SetState(EnemyState.DEAD);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.WALKING:
                if ((baseTransform.position - transform.position).sqrMagnitude < baseAttackDistance * baseAttackDistance)
                    SetState(EnemyState.ATTACK_BASE);

                break;
            case EnemyState.ATTACK_PLAYER:
                break;
            case EnemyState.ATTACK_BASE:
                break;
            case EnemyState.CHASING:
                if ((aggroTarget.position - transform.position).sqrMagnitude < playerAttackDistance * playerAttackDistance)
                    SetState(EnemyState.ATTACK_PLAYER);
                else if ((aggroTarget.position - transform.position).sqrMagnitude > playerChaseStopDistance * playerChaseStopDistance)
                    SetState(EnemyState.WALKING);
                else
                    agent.destination = aggroTarget.position;

                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
    }

    private void SetState(EnemyState newState)
    {
        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
                anim.SetBool(attackAnimatorParam, false);
                break;
            case EnemyState.ATTACK_BASE:
                anim.SetBool(attackAnimatorParam, false);
                break;
            case EnemyState.CHASING:
                anim.SetBool(chasingAnimatorParam, false);
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
                agent.stoppingDistance = baseAttackDistance;
                agent.speed = speed;
                aggroTarget = null;
                break;
            case EnemyState.ATTACK_PLAYER:
                anim.SetBool(attackAnimatorParam, true);
                agent.stoppingDistance = playerAttackDistance;
                break;
            case EnemyState.ATTACK_BASE:
                anim.SetBool(attackAnimatorParam, true);
                agent.destination = baseTransform.position;
                agent.stoppingDistance = baseAttackDistance;
                break;
            case EnemyState.CHASING:
                agent.speed = chaseSpeed;
                agent.stoppingDistance = playerAttackDistance;
                anim.SetBool(chasingAnimatorParam, true);
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

    void OnDestroy()
    {
        health.onDeath -= Die;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentState == EnemyState.WALKING)
        {
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
                if (playerHealth.health <= 0)
                {
                    SetState(EnemyState.WALKING);
                    return;
                }

                if ((aggroTarget.position - transform.position).sqrMagnitude > playerAttackDistance * playerAttackDistance)
                    SetState(EnemyState.CHASING);

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
                if (aggroTarget)
                    SetState(EnemyState.CHASING);
                else // Previous target was destroyed
                    SetState(EnemyState.WALKING);

                break;
            default:
                break;
        }
    }
}
