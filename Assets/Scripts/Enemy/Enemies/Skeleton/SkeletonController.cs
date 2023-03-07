using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonController : EnemyLight
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
    private static readonly int deadAnimatorParam = Animator.StringToHash("Dead");
    private static readonly int deathModeAnimatorParam = Animator.StringToHash("DeathMode");

    protected override void Start()
    {
        base.Start();

        agent.speed = walkSpeed;

        AnimationSetup();
        // Set random animation start time for current animation state
        anim.Play(0, -1, Random.value);
        healthLogic.onDeath += AchievementDeath;
    }

    private void AchievementDeath(DamageInfo dmg)
    {
        SteamManager.Singleton.SetAchievement("SKELETON");
    }

    private void AnimationSetup()
    {
        // Set random walk animation
        anim.SetFloat(walkModeAnimatorParam, Random.Range(0, 3));
    }
    protected override void SetRagdollAnimatorParams()
    {
        base.SetRagdollAnimatorParams();

        anim.SetBool(deadAnimatorParam, true);
        // Set random death animation
        anim.SetFloat(deathModeAnimatorParam, Random.Range(0, 8));
    }

    void FixedUpdate()
    {
        if (currentState == EnemyState.DEAD)
            return;

        HasLostTargetCheck();

        if (CurrentTarget)
            MakeDecisionOnTarget();
    }

    private void HasLostTargetCheck()
    {
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

        if (currentState != EnemyState.DEAD)
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
                Destroy(gameObject, durationBeforeDespawn);
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
                playerHealth.OnReceiveDamage(this, attackDamage);
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
                baseHealth?.OnReceiveDamage(this, attackDamage);
                break;
            case EnemyState.CHASING:
                break;
            case EnemyState.DEAD:
                break;
        }
    }
}
