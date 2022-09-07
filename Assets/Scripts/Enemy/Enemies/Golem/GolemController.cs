using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GolemState
{
    WAITING,
    BUSY,
    SHIMMY,
    AIRBORNE,
    AGGRO,
    ATTACKING_BASE,
}

public class GolemController : Enemy
{
    [SerializeField]
    private float jumpLength = 1f;

    [SerializeField]
    private float jumpTime = 0.62f;

    [SerializeField]
    private float shimmySpeed = 250f;

    [SerializeField]
    private float timeBetweenJumps = 2;

    [TextLabel(greyedOut = true), SerializeField]
    private GolemState currentGolemState = GolemState.WAITING;

    private Vector3 next;
    private Vector3 nextNormalized;
    private Quaternion rotationTarget;

    [TextLabel(greyedOut = true), SerializeField]
    private bool canJump = true;

    [SerializeField]
    private float playerDamage = default;

    [SerializeField]
    private float baseDamage = default;

    [ReadOnly, SerializeField]
    private Transform aggroTarget = default;

    [SerializeField]
    private float baseStopDistance = 1.5f;

    [SerializeField]
    private AudioSource slap = default;

    [SerializeField]
    private AudioSource headbutt = default;

    public Transform AggroTarget
    {
        get => aggroTarget;
        private set
        {
            aggroTarget = value;
            if (aggroTarget
                && aggroTarget.GetComponent<PlayerStateController>() is PlayerStateController playerController)
            {
                sync.targetPlayer = playerController.enemyViewFocus;
            } else
                sync.targetPlayer = aggroTarget;
        }
    }

    [SerializeField]
    private GolemAnimationSyncer sync = default;

    private static readonly int headButtAnimatorParam = Animator.StringToHash("HeadButt");
    private static readonly int slapRightAnimatorParam = Animator.StringToHash("SlapRight");
    private static readonly int slapLeftAnimatorParam = Animator.StringToHash("SlapLeft");
    private static readonly int shimmyAnimatorParam = Animator.StringToHash("Shimmy");
    private static readonly int jumpAnimatorParam = Animator.StringToHash("Jump");

    void FixedUpdate()
    {
        if (currentState == EnemyState.DEAD)
            return;

        switch (currentGolemState)
        {
            case GolemState.WAITING:
                CheckForNextAction();
                break;
            case GolemState.BUSY:
                break;
            case GolemState.SHIMMY:
                Shimmy();
                break;
            case GolemState.AIRBORNE:
                break;
            case GolemState.ATTACKING_BASE:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AggroTarget = other.transform;
    }

    protected override void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        if (newState == EnemyState.DEAD)
            Destroy(gameObject);
    }

    private void UpdateNextPosition()
    {
        if (agent.path.corners.Length >= 2)
            next = agent.path.corners[1] - transform.position;
        else if (CurrentTarget)
            next = CurrentTarget.position;

        next.y = 0;
        nextNormalized = next.normalized;
    }

    private void CheckForNextAction()
    {
        UpdateNextPosition();
        if (CurrentTarget && transform.position.DistanceLessThan(baseStopDistance, CurrentTarget.position))
        {
            currentGolemState = GolemState.ATTACKING_BASE;
            AggroTarget = BaseController.Singleton.transform;
            Slap(AggroTarget.position);
            return;
        }

        if (AggroTarget)
        {
            currentGolemState = GolemState.AGGRO;
            Slap(AggroTarget.position);
        } else if (Vector3.Dot(transform.forward, next.normalized) >= 0.98f)
        {
            if (canJump)
            {
                anim.SetTrigger(jumpAnimatorParam);
                Invoke(nameof(ResetJump), timeBetweenJumps);
                Invoke(nameof(FinishJumpAnim), 1.2f);
                currentGolemState = GolemState.BUSY;
            }
        } else
        {
            currentGolemState = GolemState.SHIMMY;
            anim.SetBool(shimmyAnimatorParam, true);
            rotationTarget = Quaternion.LookRotation(nextNormalized, Vector3.up);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void FinishJumpAnim()
    {
        currentGolemState = GolemState.WAITING;
    }

    public void JumpTrigger()
    {
        canJump = false;
        currentGolemState = GolemState.AIRBORNE;
        Vector3 target = nextNormalized * jumpLength;
        JumpTo(transform.position + target, jumpTime);
    }

    private void JumpTo(Vector3 pos, float time)
    {
        LeanTween.move(gameObject, pos, time);
    }

    private void Shimmy()
    {
        if (AggroTarget)
        {
            currentGolemState = GolemState.WAITING;
            anim.SetBool(shimmyAnimatorParam, false);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, shimmySpeed * Time.fixedDeltaTime);
        if (next == Vector3.zero || Vector3.Dot(transform.forward, nextNormalized) >= 0.98)
        {
            currentGolemState = GolemState.WAITING;
            anim.SetBool(shimmyAnimatorParam, false);
        }
    }

    private void Slap(Vector3 target)
    {
        Vector3 diff = target - transform.position;

        float angle = Mathf.Repeat(-Mathf.Atan2(-diff.x, diff.z) * Mathf.Rad2Deg - transform.rotation.eulerAngles.y, 360);

        if (angle < 15f || angle > 345f)
        {
            anim.SetTrigger(headButtAnimatorParam);
            headbutt.PlayDelayed(0.45f);
        } else if (angle < 180f)
        {
            anim.SetTrigger(slapRightAnimatorParam);
            slap.Play();
            SteamManager.Singleton.setAchievement("ACH_SLAPPED_GOLEM");
        } else
        {
            anim.SetTrigger(slapLeftAnimatorParam);
            slap.Play();
            SteamManager.Singleton.setAchievement("ACH_SLAPPED_GOLEM");
        }
    }

    public void OnAttackFinish()
    {
        if (currentGolemState == GolemState.AGGRO
            || currentGolemState == GolemState.ATTACKING_BASE)
        {
            currentGolemState = GolemState.WAITING;
            AggroTarget = null;
        }
    }

    public void OnDealDamage()
    {
        if (!AggroTarget)
            return;

        float damage = currentGolemState == GolemState.ATTACKING_BASE ? baseDamage : playerDamage;
        HealthLogic targetHealth = AggroTarget.GetComponent<HealthLogic>();
        Vector3 knockBackDir = (AggroTarget.position - transform.position + Vector3.up * 2).normalized;
        targetHealth.OnReceiveDamage(this, damage, knockBackDir, 10f);
    }
}
