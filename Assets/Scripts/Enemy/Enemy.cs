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

[RequireComponent(typeof(NavMeshAgent), typeof(HealthLogic))]
public abstract class Enemy : MonoBehaviour
{
    [ReadOnly, SerializeField]
    protected Transform _currentTarget;

    [ReadOnly, SerializeField]
    protected Transform baseTransform;

    [ReadOnly, SerializeField]
    protected EnemyState currentState = EnemyState.WALKING;

    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected float walkSpeed = 1.8f;

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

    protected Transform CurrentTarget
    {
        get => _currentTarget;
        set
        {
            _currentTarget = value;
            if (_currentTarget)
            {
                agent.destination = _currentTarget.position;
                if (agent.isStopped)
                    agent.isStopped = false;
            } else
            {
                agent.destination = agent.nextPosition;
                agent.isStopped = true;
            }
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Die;
    }

    void OnDestroy()
    {
        health.onDeath -= Die;
    }

    private void Die(DamageInfo dmg)
    {
        SetState(EnemyState.DEAD);
    }

    void Start()
    {
        baseTransform = BaseController.Singleton.transform;
        CurrentTarget = baseTransform;
        baseHealth = baseTransform.GetComponent<HealthLogic>();

        agent.speed = walkSpeed;

        AnimationSetup();
        // Set random animation start time for current animation state
        anim.Play(0, -1, Random.value);
    }

    protected virtual void AnimationSetup()
    {}

    void FixedUpdate()
    {
        HasLostTargetCheck();

        if (CurrentTarget)
            MakeDecisionOnTarget();
    }

    private void MakeDecisionOnTarget()
    {
        switch (currentState)
        {
            case EnemyState.WALKING:
                if (transform.position.DistanceLessThan(baseAttackDistance, baseTransform.position))
                    SetState(EnemyState.ATTACK_BASE);

                break;
            case EnemyState.ATTACK_PLAYER:
                break;
            case EnemyState.ATTACK_BASE:
                break;
            case EnemyState.CHASING:
                if (transform.position.DistanceLessThan(playerAttackDistance, CurrentTarget.position))
                    SetState(EnemyState.ATTACK_PLAYER);
                else if (transform.position.DistanceGreaterThan(playerChaseStopDistance, CurrentTarget.position))
                    SetState(EnemyState.WALKING);
                else
                    UpdateTargetDestination();

                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
    }

    private void HasLostTargetCheck()
    {
        if (currentState == EnemyState.DEAD)
            return;

        if (!CurrentTarget) // Previous target was destroyed
        {
            if (baseTransform)
            {
                CurrentTarget = baseTransform;
                SetState(EnemyState.WALKING);
            } else
                CurrentTarget = null;
        }
    }

    private void SetState(EnemyState newState)
    {
        if (newState == currentState)
            return;

        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
            case EnemyState.ATTACK_BASE:
                anim.SetBool(attackAnimatorParam, false);
                break;
            case EnemyState.CHASING:
                agent.speed = walkSpeed;
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
                CurrentTarget = baseTransform;
                agent.stoppingDistance = baseAttackDistance;
                break;
            case EnemyState.ATTACK_PLAYER:
                agent.stoppingDistance = playerAttackDistance;
                anim.SetBool(attackAnimatorParam, true);
                break;
            case EnemyState.ATTACK_BASE:
                CurrentTarget = baseTransform;
                agent.stoppingDistance = baseAttackDistance;
                anim.SetBool(attackAnimatorParam, true);
                break;
            case EnemyState.CHASING:
                agent.speed = chaseSpeed;
                agent.stoppingDistance = playerAttackDistance;
                anim.SetBool(chasingAnimatorParam, true);
                break;
            case EnemyState.DEAD:
                CurrentTarget = null;
                Destroy(gameObject, 2.5f);
                break;
            default:
                break;
        }

        currentState = newState;
        HasLostTargetCheck();
    }

    private void UpdateTargetDestination()
    {
        // Invoke the logic in CurrentTarget's setter
        CurrentTarget = CurrentTarget;
    }

    public void OnPlayerDetected(Transform playerTransform)
    {
        if (currentState == EnemyState.WALKING)
        {
            CurrentTarget = playerTransform;
            SetState(EnemyState.CHASING);
        }
    }

    public void DealDamage()
    {
        switch (currentState)
        {
            case EnemyState.ATTACK_PLAYER:
                HealthLogic playerHealth = CurrentTarget.GetComponent<HealthLogic>();
                playerHealth.DealDamage(attackDamage);
                if (playerHealth.health <= 0)
                {
                    SetState(EnemyState.WALKING);
                    return;
                }

                if (transform.position.DistanceGreaterThan(playerAttackDistance, CurrentTarget.position))
                    SetState(EnemyState.CHASING);

                break;
            case EnemyState.ATTACK_BASE:
                baseHealth?.DealDamage(attackDamage);
                break;
            default:
                break;
        }
    }
}
