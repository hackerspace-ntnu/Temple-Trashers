using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


public class GolemnController : MonoBehaviour
{
    [SerializeField]
    private float jumpLength = 1f, jumpTime = 0.62f, shimmySpeed = 250f;
    private NavMeshAgent agent;
    private Animator anim;
    private states state = states.Busy;
    private Vector3 next, nextNormalized;
    private Quaternion rotationTarget;
    [SerializeField]
    private AnimationCurve slapCurve;

    [SerializeField]
    private Transform slapCity;

    [SerializeField]
    private OverrideTransform rightRig, leftRig;

    private Transform player;

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
        anim = GetComponent<Animator>();
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

    private void CheckForNextAction()
    {
        next = agent.path.corners[1] - transform.position;
        next.y = 0;
        nextNormalized = next.normalized;
        if(player != null)
        {
            state = states.Aggro;
            Slap(player.position);
        }
        else if (Vector3.Dot(transform.forward, next.normalized) >= 0.98f)
        {
            anim.SetTrigger("Jump");
            state = states.Busy;
        }
        else
        {
            state = states.Shimmy;
            anim.SetBool("Shimmy", true);
            rotationTarget = Quaternion.LookRotation(nextNormalized, Vector3.up);
        }
    }
    public void JumpTrigger()
    {
        state = states.Airborne;
        Vector3 target = next.sqrMagnitude > jumpLength * jumpLength ? nextNormalized * jumpLength : next;
        JumpTo(transform.position + target, jumpTime);
    }

    public void SpawnTrigger()
    {
        state = states.Waiting;
    }

    private void JumpTo(Vector3 pos, float time) {
        LeanTween.move(gameObject, pos, time).setOnComplete(() => state = states.Waiting);     
    }
    private void Shimmy()
    {

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, shimmySpeed * Time.fixedDeltaTime);
        if(next == Vector3.zero || Vector3.Dot(transform.forward, nextNormalized) >= 0.98)
        {
            state = states.Waiting;
            anim.SetBool("Shimmy", false);
        }
    }
    private void Slap(Vector3 target)
    {
        anim.SetTrigger("Slap");
        Vector3 orig = slapCity.localPosition;
        Vector3 diff = target - transform.position;
        
        float angle = Mathf.Repeat(-Mathf.Atan2(-diff.x, diff.z)*180/Mathf.PI - transform.rotation.eulerAngles.y, 360);
        
        
        print(angle);
        bool right = angle < 180;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            if (state == states.Airborne)
            {
                player = other.transform;
            }

            else
            {
                state = states.Aggro;
                Slap(other.transform.position);
            }
        }
    }
}
