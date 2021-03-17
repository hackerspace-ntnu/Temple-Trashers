using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    private HealthLogic health; // Refrence to the health script
    private PlayerStates currentState = PlayerStates.Free; // The current player state
    private PlayerInput input;  // Controller input
    private PlayerManager manager;
    private PlayerMotion motion;
    private PlayerUi ui;

    private Vector2 moveInput = Vector2.zero, aimInput = Vector2.zero;
    private bool interact = false, back = false, select = false;

    private List<Interactable> interactables = new List<Interactable>(); // List of interactables in range
    private Interactable focusedInteractable;   // The current focused interactable
    private GameObject liftedObject;            // Object being lifted
    public Transform inventory;                 // Where items are carried

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
                ui.Select();
                motion.move();
                if (!Select) {
                    if (ui.GetSelectedSegment()) {
                        Lift(ui.GetSelectedSegment());
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

        UpdateFocusedInteractable();        
    }




    public void SetStateFree()
    {
        SetState(PlayerStates.Free);
    }

    private void Die()
    {
        // Drop anything we are carrying
        if (liftedObject != null) 
        {
            liftedObject.GetComponent<Interactable>().Interact(this);
            SetFocusedInteractable(null);

        }
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
        input.actions["Interact"].performed += ctx => OnInteract();
        input.actions["Back"].performed += ctx => back = true;
        input.actions["Select"].performed += ctx => select = true;
        input.actions["Select"].canceled += ctx => select = false;
    }

    private void OnInteract()   // Gets called when interact button is pressed
    {
        if(focusedInteractable != null)
        {
            focusedInteractable.Interact(this); // interact with the current target
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable inter = other.GetComponentInParent<Interactable>();
        if (inter != null && inter.canInteract)
        {
            AddInteractable(inter); 
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            RemoveInteractable(other.GetComponentInParent<Interactable>());
        }
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


    public void AddInteractable(Interactable a)
    {
        if (interactables.Contains(a)) // Do not add an interactable twice
        {
            return;
        }
        interactables.Add(a);
    }

    public void RemoveInteractable(Interactable a)
    {
        interactables.Remove(a);
    }

    // Update the focused interactable based on distance from player
    private void UpdateFocusedInteractable()
    {
        if(interactables.Count == 0) 
        {
            if (focusedInteractable != null) // The object we were focusing is no longer focusable
            {
                focusedInteractable.Unfocus(this);
                focusedInteractable = null;
            }
            // Otherwise do nothing
            return;
        }
        else if(interactables.Count == 1) // If there is only one interactable object select that object
        {
            SetFocusedInteractable(interactables[0]);
            return;
        }
        else if(focusedInteractable == null) // Wait until we have at least one object to interact with
        {
            return;
        }

        // Lifted objects are top priority
        if (liftedObject != null && focusedInteractable != liftedObject)
        {
            SetFocusedInteractable(liftedObject.GetComponent<Interactable>());
            return;
        }

        // Otherwise get closest interactable
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
        SetFocusedInteractable(closest);
    }

    private void SetFocusedInteractable(Interactable a)
    {
        if(focusedInteractable == a) { return; } // This is allready the focused interactable
        focusedInteractable?.Unfocus(this);
        a?.Focus(this);
        focusedInteractable = a;
    }

    public void Lift(GameObject a)
    {
        liftedObject = a;
        SetState(PlayerStates.Lifting);
    }

    public void Drop(GameObject a)
    {
        liftedObject = null;
        SetState(PlayerStates.Free);
        if (!a.GetComponent<Interactable>().canInteract) // Check if we can still interact with the object
        {
            RemoveInteractable(a.GetComponent<Interactable>());
        }
    }

    public Vector2 MoveInput { get => moveInput; }
    public Vector2 AimInput { get => aimInput; }
    public bool Interact { get => interact; }
    public bool Back { get => back; }
    public bool Select { get => select; }

    
}
