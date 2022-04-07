using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SkeletonController : Enemy
{
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
    private AudioSource audioSource = default;

    private static readonly int attackAnimatorParam = Animator.StringToHash("Attack");
    private static readonly int chasingAnimatorParam = Animator.StringToHash("Chasing");
    private static readonly int walkModeAnimatorParam = Animator.StringToHash("WalkMode");

    protected override void Die(DamageInfo damageInfo)
    {
        base.Die(damageInfo);

        UIManager.Singleton.IncreaseScore(scoreValue);
    }

    protected override void Start()
    {
        base.Start();

        agent.speed = walkSpeed;

        AnimationSetup();
        // Set random animation start time for current animation state
        anim.Play(0, -1, Random.value);
    }

    private void AnimationSetup()
    {
        // Set random walk animation
        anim.SetFloat(walkModeAnimatorParam, Mathf.Floor(Random.Range(0, 2)));
    }

    void FixedUpdate()
    {
        HasLostTargetCheck();

        if (CurrentTarget)
            MakeDecisionOnTarget();
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
        }
    }

    protected override void SetState(EnemyState newState)
    {
        base.SetState(newState);

        HasLostTargetCheck();
    }

    protected override void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        switch (oldState)
        {
            case EnemyState.WALKING:
                break;
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
        }
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

    public void OnDealDamage()
    {
        switch (currentState)
        {
            case EnemyState.WALKING:
                break;
            case EnemyState.ATTACK_PLAYER:
                HealthLogic playerHealth = CurrentTarget.GetComponent<HealthLogic>();
                playerHealth.OnReceiveDamage(attackDamage);
                audioSource.Play();
                if (playerHealth.health <= 0)
                {
                    SetState(EnemyState.WALKING);
                    return;
                }

                if (transform.position.DistanceGreaterThan(playerAttackDistance, CurrentTarget.position))
                    SetState(EnemyState.CHASING);

                break;
            case EnemyState.ATTACK_BASE:
                baseHealth?.OnReceiveDamage(attackDamage);
                break;
            case EnemyState.CHASING:
                break;
            case EnemyState.DEAD:
                break;
        }
    }
}
