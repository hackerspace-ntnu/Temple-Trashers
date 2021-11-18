using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    WALKING,
    ATTACK_PLAYER,
    ATTACK_BASE,
    CHASING,
    DEAD,
}

public abstract class Enemy : MonoBehaviour
{
    [ReadOnly, SerializeField]
    protected Transform currentTarget;

    [ReadOnly, SerializeField]
    protected Transform baseTransform;

    [ReadOnly, SerializeField]
    protected EnemyState currentState = EnemyState.WALKING;

    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected float speed = 5f;

    [SerializeField]
    protected float chaseSpeed = 5f;

    [SerializeField]
    protected float baseAttackDistance = 2f;

    [SerializeField]
    protected float playerAttackDistance = 1f;

    [SerializeField]
    protected float playerChaseStopDistance = 4f;

    [SerializeField]
    protected float attackDamage = 1f;

    protected NavMeshAgent agent;
    protected HealthLogic health;
    protected HealthLogic baseHealth;

    protected static readonly int attackAnimatorParam = Animator.StringToHash("Attack");
    protected static readonly int chasingAnimatorParam = Animator.StringToHash("Chasing");

    void Start()
    {
        baseTransform = BaseController.Singleton.transform;
        currentTarget = baseTransform;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.destination = currentTarget.position;

        health = GetComponent<HealthLogic>();
        health.onDeath += Die;
        baseHealth = baseTransform.GetComponent<HealthLogic>();

        AnimationSetup();
        // Set random animation start time for current animation state
        anim.Play(0, -1, Random.value);
    }

    protected virtual void AnimationSetup()
    {}

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
                if ((currentTarget.position - transform.position).sqrMagnitude < playerAttackDistance * playerAttackDistance)
                    SetState(EnemyState.ATTACK_PLAYER);
                else if ((currentTarget.position - transform.position).sqrMagnitude > playerChaseStopDistance * playerChaseStopDistance)
                    SetState(EnemyState.WALKING);
                else
                    agent.destination = currentTarget.position;

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
                currentTarget = null;
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

    public void OnPlayerDetected(Transform playerTransform)
    {
        if (currentState == EnemyState.WALKING)
        {
            currentTarget = playerTransform;
            SetState(EnemyState.CHASING);
        }
    }

    public void DealDamage()
    {
        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
                HealthLogic playerHealth = currentTarget.GetComponent<HealthLogic>();
                playerHealth.DealDamage(attackDamage);
                if (playerHealth.health <= 0)
                {
                    SetState(EnemyState.WALKING);
                    return;
                }

                if ((currentTarget.position - transform.position).sqrMagnitude > playerAttackDistance * playerAttackDistance)
                    SetState(EnemyState.CHASING);

                break;
            case EnemyState.ATTACK_BASE:
                baseHealth?.DealDamage(attackDamage);
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
                if (currentTarget)
                    SetState(EnemyState.CHASING);
                else // Previous target was destroyed
                    SetState(EnemyState.WALKING);

                break;
            default:
                break;
        }
    }
}
