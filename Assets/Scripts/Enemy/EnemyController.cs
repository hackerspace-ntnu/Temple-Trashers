using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerBase;
    public float movementSpeed = 5;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateTowardsTarget(playerBase);
    }

    void FixedUpdate()
    {
        Vector3 directionToBase = (playerBase.position - transform.position).normalized;
        MoveTowardsTarget(directionToBase);
    }

    private void MoveTowardsTarget(Vector3 direction)
    {
        Vector3 targetVelocity = movementSpeed * direction;
        Vector3 targetVelocityDiff = targetVelocity - rigidbody.velocity;
        // Prevent movement along the y axis
        targetVelocityDiff.y = 0;
        rigidbody.AddForce(targetVelocityDiff, ForceMode.Impulse);
    }

    private void RotateTowardsTarget(Transform target)
    {
        transform.LookAt(target);
        // Set x and y rotation to 0, so that the transform is only rotated around the y axis
        transform.eulerAngles = transform.eulerAngles.SetOnly(x: 0, z: 0);
    }
}
