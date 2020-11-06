using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PlayerDetector : MonoBehaviour
{
    public EnemyController controller;

    void OnTriggerEnter(Collider collider)
    {
        // If the colliding object is a player:
        if (collider.GetComponent<PlayerStateController>())
            controller.OnPlayerDetected(collider.transform);
    }
}
