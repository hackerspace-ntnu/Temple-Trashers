using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnigolemController : MonoBehaviour
{

    [SerializeField]
    private Transform model;

    private Transform baseTransform;

    [SerializeField]
    private float tiltAngleSide = 30f;

    [SerializeField]
    private float tiltAngleFront = 30f;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float detonationDistance = 2f;

    private NavMeshAgent agent;

    private Vector3 lastForward;



    private void Start()
    {
        lastForward = transform.forward;
        baseTransform = BaseController.Singleton.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(baseTransform.position);

        GetComponent<HealthLogic>().onDeath += (damageInfo) => Destroy(gameObject);
    }

    
    void Update()
    {

        float angularSpeed = Vector3.Cross(lastForward, transform.forward).y / Time.deltaTime;
        lastForward = transform.forward;

        float sidewaysAngle = -tiltAngleSide * angularSpeed;
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed);
        float forwardAngle = tiltAngleFront * speed;

        model.localRotation = Quaternion.Euler(forwardAngle, 0, sidewaysAngle);

        if(Vector3.Distance(transform.position, baseTransform.position) < detonationDistance)
        {
            Detonate();
        }
    }
    void Detonate()
    {
        baseTransform.GetComponent<HealthLogic>()?.DealDamage(damage);
        Destroy(gameObject);
    }
}
