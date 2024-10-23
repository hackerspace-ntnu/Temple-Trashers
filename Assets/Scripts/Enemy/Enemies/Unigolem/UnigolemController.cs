using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnigolemController : EnemyLight
{
    [SerializeField]
    private Transform model = default;

    [SerializeField]
    private float tiltAngleSide = 30f;

    [SerializeField]
    private float tiltAngleFront = 30f;

    [SerializeField]
    private float detonationDistance = 2f;

    private Vector3 lastForward;

    private static readonly int speedAnimatorParam = Animator.StringToHash("Speed");

    private float lastSidewaysTilt = 0;
    private float sidewaysTiltSmoothing = 0.9f;

    protected override void Start()
    {
        base.Start();

        CurrentTarget = baseTransform;
        lastForward = transform.forward;
    }

    protected override void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.WALKING:
            case EnemyState.ATTACK_PLAYER:
            case EnemyState.ATTACK_BASE:
            case EnemyState.CHASING:
                break;
            case EnemyState.DEAD:
                anim.enabled = false;
                Destroy(gameObject, durationBeforeDespawn);
                break;
        }
    }

    void FixedUpdate()
    {
        if (currentState == EnemyState.DEAD)
            return;

        float angularSpeed = Vector3.Cross(lastForward, transform.forward).y / Time.fixedDeltaTime;
        lastForward = transform.forward;

        float sidewaysAngle = -tiltAngleSide * angularSpeed;
        lastSidewaysTilt = lastSidewaysTilt * sidewaysTiltSmoothing + sidewaysAngle * (1 - sidewaysTiltSmoothing);
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat(speedAnimatorParam, speed);
        float forwardAngle = tiltAngleFront * speed;

        model.localRotation = Quaternion.Euler(forwardAngle, 0, lastSidewaysTilt);

        if (baseTransform == null) { return; }

        if (Vector3.Distance(transform.position, baseTransform.position) < detonationDistance)
            baseHealth?.OnReceiveDamage(this, attackDamage);
    }

    protected override Vector3 GetLaunchTorque()
    {
        // Using `forward` here would have made the most sense to me
        // (getting the body to rotate in the same direction as this collider is facing),
        // but that would have caused the body to rotate in its left-facing direction
        return agent.speed * colliderToEnable.transform.right;
    }
}
