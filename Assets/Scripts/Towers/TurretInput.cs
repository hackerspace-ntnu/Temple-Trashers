using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInput : MonoBehaviour
{
    private PlayerStateController player;

    void Start()
    {
        player = GetComponent<PlayerStateController>();
    }

    public Vector2 GetAimInput()
    {
        return player.AimInput;
    }
}
