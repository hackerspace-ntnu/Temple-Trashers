using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GolemController : Enemy
{
    [SerializeField]
    private float jumpLength = 1f, jumpTime = 0.62f, shimmySpeed = 250f, timeBetweenJumps = 2;
    private states state = states.Waiting;
    private Vector3 next, nextNormalized;
    private Quaternion rotationTarget;
    private bool canJump = true;
    [SerializeField]
    private float playerDamage;

    [SerializeField]
    private float baseDamage;

    private Transform aggroTarget;

    [SerializeField]
    private float baseStopDistance = 1.5f;

    public Transform AggroTarget {
        get { return aggroTarget; }
        private set {
            aggroTarget = value;
            var playerController = value.GetComponent<PlayerStateController>();
            if (playerController)
            {
                sync.targetPlayer = playerController.EnemyViewFocus;
            }
            else
            {
                sync.targetPlayer = value;
            }

        }
    }

    [SerializeField]
    private GolemAnimationSyncer sync;

    private enum states
    {
        Waiting,
        Busy,
        Shimmy,
        Airborne,
        Aggro,
        AttackingBase
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.destination = BaseController.Singleton.transform.position;
        GetComponent<HealthLogic>().onDeath += (dmg) => Destroy(gameObject);
    }
   
    private void FixedUpdate()
    {
        switch (state)
        {
            case states.Waiting:
                CheckForNextAction();
                break;
            case states.Busy:
                break;
            case states.Shimmy:
                Shimmy();
                break;
            case states.Airborne:
                break;
            case states.AttackingBase:
                break;
            default:
                break;
        }
    }

    private void updateNextPosition()
    {
        if (agent.path.corners.Length >= 2)
        {
            next = agent.path.corners[1] - transform.position;
        }
        else
        {
            next = agent.destination;
        }
        next.y = 0;
        nextNormalized = next.normalized;
    }

    private void CheckForNextAction()
    {
        updateNextPosition();
        if((agent.destination - transform.position).sqrMagnitude < Mathf.Pow(baseStopDistance, 2))
        {
            state = states.AttackingBase;
            AggroTarget = BaseController.Singleton.transform;
            Slap(AggroTarget.position);
            return;
        }
        if(AggroTarget != null)
        {
            state = states.Aggro;
            Slap(AggroTarget.position);
        }
        else if (Vector3.Dot(transform.forward, next.normalized) >= 0.98f )
        {
            if (canJump)
            {
                anim.SetTrigger("Jump");
                Invoke("ResetJump", timeBetweenJumps);
                Invoke("FinishJumpAnim", 1.2f);
                state = states.Busy;
            }
        }
        else
        {
            state = states.Shimmy;
            anim.SetBool("Shimmy", true);
            rotationTarget = Quaternion.LookRotation(nextNormalized, Vector3.up);
        }
    }

    private void ResetJump(){canJump = true;}
    private void FinishJumpAnim() { state = states.Waiting; }

    public void JumpTrigger()
    {
        canJump = false;
        state = states.Airborne;
        Vector3 target = nextNormalized * jumpLength; //: next;
        JumpTo(transform.position + target, jumpTime);
    }

    public void SpawnTrigger()
    {
        state = states.Waiting;
    }

    private void JumpTo(Vector3 pos, float time) {
        LeanTween.move(gameObject, pos, time);     
    }
    private void Shimmy()
    {
        if(AggroTarget != null)
        {
            state = states.Waiting;
            anim.SetBool("Shimmy", false);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, shimmySpeed * Time.fixedDeltaTime);
        if(next == Vector3.zero || Vector3.Dot(transform.forward, nextNormalized) >= 0.98)
        {
            state = states.Waiting;
            anim.SetBool("Shimmy", false);
        }
    }

    private void Slap(Vector3 target)
    {
        Vector3 diff = target - transform.position;
        
        float angle = Mathf.Repeat(-Mathf.Atan2(-diff.x, diff.z)*180/Mathf.PI - transform.rotation.eulerAngles.y, 360);
        
        if(angle < 15f || angle > 345f)
        {
            anim.SetTrigger("HeadButt");
        }
        else if(angle < 180f)
        {
            anim.SetTrigger("SlapRight");
        }
        else
        {
            anim.SetTrigger("SlapLeft");
        }
    }

    public void OnAttackFinish()
    {
        if(state == states.Aggro || state == states.AttackingBase)
        {
            state = states.Waiting;
            AggroTarget = null;

        }
    }

    public void DealDamage() {
        var damage = state == states.AttackingBase ? baseDamage : playerDamage;
        AggroTarget.GetComponent<HealthLogic>().OnReceiveDamage(damage, (AggroTarget.position - transform.position + Vector3.up * 2).normalized, 10f);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.tag == "Player")
        {
            AggroTarget = other.transform;    
        }
    }

    protected override void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
    }
}
