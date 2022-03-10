using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


public class GolemnController : MonoBehaviour
{
    [SerializeField]
    private float jumpLength = 1f, jumpTime = 0.62f, shimmySpeed = 250f, timeBetweenJumps = 2;
    private NavMeshAgent agent;
    [SerializeField]
    private Animator anim;
    private states state = states.Waiting;
    private Vector3 next, nextNormalized;
    private Quaternion rotationTarget;
    private bool canJump = true;
    [SerializeField]
    private float damage;

    private Transform player;

    public Transform Player {
        get { return player; }
        private set {
            player = value;
            sync.targetPlayer = value; }
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
    }
    // Start is called before the first frame update
    void Start()
    {
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
        if(Player != null)
        {
            state = states.Aggro;
            Slap(Player.position);
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
        //next.sqrMagnitude > jumpLength * jumpLength ?
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
        if(Player != null)
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
        //anim.SetTrigger("Slap");
        //Vector3 orig = slapCity.localPosition;
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

        
        /*
        Quaternion fromRot = Quaternion.identity;
        float toRot;
        if (right)
        {
            if(rightRig!= null)
            {
                rightRig.weight = 1;
            }
            toRot = angle + 120;
        }
        else
        {
            if(leftRig != null)
            {
                leftRig.weight = 1;
            }
            toRot = angle - 120;
        }

        LeanTween.value(0, 1, 1f)
        .setOnUpdate(x => {
            slapCity.localRotation = Quaternion.Euler(new Vector3(0, angle * x, 0));
            slapCity.localPosition = orig + x * (diff.y+3)*Vector3.up;
        })
        .setEase(slapCurve)
        .setOnComplete(() => {
            slapCity.localPosition = orig;
            slapCity.localRotation = fromRot;
            player = null;
        });
        */
    }
    public void OnAttackFinish()
    {
        if(state == states.Aggro)
        {
            state = states.Waiting;
            Player = null;

        }
    }

    public void DealDamage() {
        Player.GetComponent<HealthLogic>().DealDamage(damage, (Player.position - transform.position + Vector3.up * 2).normalized, 5f);
    }
    private void OnTriggerEnter(Collider other)
    { 
        if(other.tag == "Player")
        {
            Player = other.transform;    
        }
    }
}
