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

    [TextLabel(greyedOut = true), SerializeField]
    protected EnemyState currentState = EnemyState.WALKING;

    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected float attackDamage = 1f;

    [SerializeField]
    protected int scoreValue = 10;

    protected NavMeshAgent agent;
    protected HealthLogic healthLogic;

    protected Transform baseTransform;
    protected HealthLogic baseHealth;

    protected Transform CurrentTarget
    {
        get => _currentTarget;
        set
        {
            _currentTarget = value;
            if (_currentTarget)
            {
                agent.SetDestination(_currentTarget.position);
                if (agent.isStopped)
                    agent.isStopped = false;
            } else
            {
                agent.SetDestination(agent.nextPosition);
                agent.isStopped = true;
            }
        }
    }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthLogic = GetComponent<HealthLogic>();
        healthLogic.onDeath += Die;
    }

    protected virtual void OnDestroy()
    {
        healthLogic.onDeath -= Die;
    }

    protected virtual void Die(DamageInfo damageInfo)
    {
        // Don't increase the score if killed by the base's zap
        if (!(damageInfo.FromSource is BaseController))
            UIManager.Singleton.IncreaseScore(scoreValue);

        SetState(EnemyState.DEAD);
    }

    protected virtual void Start()
    {
        baseTransform = BaseController.Singleton.transform;
        CurrentTarget = baseTransform;
        baseHealth = baseTransform.GetComponent<HealthLogic>();
    }

    protected virtual void SetState(EnemyState newState)
    {
        if (newState == currentState)
            return;

        HandleStateChange(currentState, newState);
        currentState = newState;
    }

    protected abstract void HandleStateChange(EnemyState oldState, EnemyState newState);
}
