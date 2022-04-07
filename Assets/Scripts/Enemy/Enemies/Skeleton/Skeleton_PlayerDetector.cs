using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton_PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private SkeletonController skeleton = default;

    void OnTriggerEnter(Collider collider)
    {
        // If the colliding object is a player:
        if (collider.CompareTag("Player"))
            skeleton.OnPlayerDetected(collider.transform);
    }
}
