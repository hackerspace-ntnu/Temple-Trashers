using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private HealthLogic health;
    private PlayerStates currentState = PlayerStates.Alive;

    [SerializeField]
    private SkinnedMeshRenderer mesh;
    public enum PlayerStates
    {
        Lifting,
        Dead,
        Alive
    }

    private void Start()
    {
        health = GetComponent<HealthLogic>();
        health.OnDeath += die; 
    }

    private void die()
    {
        if(currentState == PlayerStates.Dead) { return; }
        setState(PlayerStates.Dead);
        PlayerRespawnController.Instance.waitForRespawn(this);
    }

    private void setState(PlayerStates state)
    {
        if(currentState == state) { return; }

        switch (currentState)
        {
            case PlayerStates.Dead:
                health.Heal(health.maxHealth);
                mesh.enabled = true;
                //Spill av revive anim
                break;
            default:
                break;
        }

        switch (state)
        {
            case PlayerStates.Lifting:
                break;
            case PlayerStates.Dead:
                mesh.enabled = false;
                //Spill av døds anim
                break;
            case PlayerStates.Alive:
                break;
            default:
                break;
        }
        currentState = state;
    }
    public void revive()
    {
        
        if(currentState == PlayerStates.Dead)
        {
            setState(PlayerStates.Alive);
        }
    }

    public PlayerStates CurrentState { get => currentState; }
}
