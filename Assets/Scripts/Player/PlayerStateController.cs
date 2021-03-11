using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    private HealthLogic health;
    private PlayerStates currentState = PlayerStates.Free;
    private PlayerInput input;
    private PlayerManager manager;
    private PlayerMotion motion;
    private PlayerUi ui;

    private Vector2 moveInput = Vector2.zero, aimInput = Vector2.zero;
    private bool interact = false, back = false, select = false;

    private List<Interactable> interactables = new List<Interactable>();
    private Interactable focusedInteractable;
    private bool liftingInteractable = false;
    private GameObject liftedObject;

    [SerializeField]
    private SkinnedMeshRenderer mesh;
    public enum PlayerStates
    {
        InAnimation,
        Lifting,
        Dead,
        Free,
        Building,
        InTurretMenu
    }

    private void Start()
    {
        motion = GetComponent<PlayerMotion>();
        health = GetComponent<HealthLogic>();
        health.OnDeath += Die;
        ui = GetComponent<PlayerUi>();
    }
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerStates.InAnimation:
                break;
            case PlayerStates.Lifting:
                motion.move();
                break;
            case PlayerStates.Dead:
                break;
            case PlayerStates.Free:
                if (Select){ SetState(PlayerStates.InTurretMenu);}
                else { 
                motion.move();
                }
                break;
            case PlayerStates.InTurretMenu:
                ui.select();

                if (!Select) {
                    if (ui.getSelectedSegment()) {
                        lift(ui.getSelectedSegment());
                        }
                    else
                    {
                        SetState(PlayerStates.Free);
                    }
                }
                break;
            default:
                break;
        }
    }




    public void setStateFree()
    {
        SetState(PlayerStates.Free);
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
            case PlayerStates.Free:
                break;
            case PlayerStates.InTurretMenu:
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
        input.actions["Select"].canceled += ctx => select = false;
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
        input.actions["Select"].canceled -= ctx => select = false;
    }


    public void addInteractable(Interactable a)
    {
        if (interactables.Contains(a))
        {
            Debug.LogWarning("Tried to add interactable twice to same player: " + a);
            return;
        }
        interactables.Add(a);
    }

    public void removeInteractable(Interactable a)
    {
        interactables.Remove(a);
    }

    private void updateFocusedInteractable()
    {
        //Lifted objects take top priority
        if (liftingInteractable)
        {
            if (focusedInteractable == null)
            {
                liftingInteractable = false;
            }
            else
            {
                return;
            }
        }

        //Get closest interactable otherwise
        Interactable closest = focusedInteractable;
        float dist = (focusedInteractable.transform.position - transform.position).sqrMagnitude;
        foreach (Interactable i in interactables)
        {
            if ((i.transform.position - transform.position).sqrMagnitude < dist)
            {
                dist = (i.transform.position - transform.position).sqrMagnitude;
                closest = i;
            }
        }
        setFocusedInteractable(closest);
    }
    private void setFocusedInteractable(Interactable a)
    {
        if(focusedInteractable == a) { return; }
        focusedInteractable?.unFocus(this);
        a?.focus(this);
        focusedInteractable = a;
    }
    public void lift(GameObject a)
    {
        liftedObject = a;
        setFocusedInteractable(a.GetComponent<Interactable>());
        liftingInteractable = true;
        SetState(PlayerStates.Lifting);
        //Might set script to remove rigidbody of lifted object
    }


    public Vector2 MoveInput { get => moveInput; }
    public Vector2 AimInput { get => aimInput; }
    public bool Interact { get => interact; }
    public bool Back { get => back; }
    public bool Select { get => select; }

    
}
