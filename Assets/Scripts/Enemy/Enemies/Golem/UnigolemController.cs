using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnigolemController : Enemy
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
                Destroy(gameObject);
                break;
        }
    }

    void FixedUpdate()
    {
        float angularSpeed = Vector3.Cross(lastForward, transform.forward).y / Time.deltaTime;
        lastForward = transform.forward;

        float sidewaysAngle = -tiltAngleSide * angularSpeed;
        lastSidewaysTilt = lastSidewaysTilt * sidewaysTiltSmoothing + sidewaysAngle * (1 - lastSidewaysTilt); 
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat(speedAnimatorParam, speed);
        float forwardAngle = tiltAngleFront * speed;

        model.localRotation = Quaternion.Euler(forwardAngle, 0, lastSidewaysTilt);

        if (Vector3.Distance(transform.position, baseTransform.position) < detonationDistance)
            Detonate();
    }

    private void Detonate()
    {
        baseHealth?.OnReceiveDamage(attackDamage);
        Destroy(gameObject);
    }
}
