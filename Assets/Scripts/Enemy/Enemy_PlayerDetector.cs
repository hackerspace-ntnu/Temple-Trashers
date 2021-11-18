using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PlayerDetector : MonoBehaviour
{
    public Enemy enemy;

    void OnTriggerEnter(Collider collider)
    {
        // If the colliding object is a player:
        if (collider.CompareTag("Player"))
            enemy.OnPlayerDetected(collider.transform);
    }
}
