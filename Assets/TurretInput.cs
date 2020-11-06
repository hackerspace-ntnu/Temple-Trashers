using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInput : MonoBehaviour
{
    PlayerStateController player;
    void Start()
    {
        player = GetComponent<PlayerStateController>();
    }
    public Vector2 GetAimInput()
    {
        if(player.CurrentState == PlayerStateController.PlayerStates.Dead)
        {
            return Vector2.zero;
        }
        return player.AimInput;
    }
    public PlayerStateController.PlayerStates CurrentPlayerState()
    {
        return player.CurrentState;
    }
}
