using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    private HealthLogic health;
    private PlayerStates currentState = PlayerStates.Alive;
    private PlayerInput input;
    private PlayerManager manager;

    private Vector2 moveInput = Vector2.zero, aimInput = Vector2.zero;
    private bool interact = false, back = false, select = false;

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
        health.OnDeath += Die; 
    }

    private void Die()
    {
        SetState(PlayerStates.Dead);
        manager.RespawnPlayer(1f);
        CameraFocusController.Instance?.removeFocusObject(transform);
        Destroy(gameObject);
    }

    private void SetState(PlayerStates state)
    {
        if(currentState == state) { return; }

        switch (currentState)
        {
            case PlayerStates.Dead:
                break;
            default:
                break;
        }

        switch (state)
        {
            case PlayerStates.Lifting:
                break;
            case PlayerStates.Dead:
                break;
            case PlayerStates.Alive:
                break;
            default:
                break;
        }
        currentState = state;
    }



    public void SetUpInput(PlayerInput input, PlayerManager manager)
    {
        this.input = input;
        this.manager = manager;
        input.actions["Move"].performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.actions["Move"].canceled += ctx => moveInput = Vector2.zero;
        input.actions["Aim"].performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        input.actions["Aim"].canceled += ctx => aimInput = Vector2.zero;
        input.actions["Interact"].performed += ctx => interact = true;
        input.actions["Back"].performed += ctx => back = true;
        input.actions["Select"].performed += ctx => select = true;
    }

    public PlayerStates CurrentState { get => currentState; }

    private void OnDestroy()
    {
        input.actions["Move"].performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        input.actions["Move"].canceled -= ctx => moveInput = Vector2.zero;
        input.actions["Aim"].performed -= ctx => aimInput = ctx.ReadValue<Vector2>();
        input.actions["Aim"].canceled -= ctx => aimInput = Vector2.zero;
        input.actions["Interact"].performed -= ctx => interact = true;
        input.actions["Back"].performed -= ctx => back = true;
        input.actions["Select"].performed -= ctx => select = true;
    }
    


    public Vector2 MoveInput { get => moveInput; }
    public Vector2 AimInput { get => aimInput; }
    public bool Interact { get => interact; }
    public bool Back { get => back; }
    public bool Select { get => select; }
}
