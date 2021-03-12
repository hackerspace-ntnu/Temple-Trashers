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
        Free
    }

    private void Start()
    {
        motion = GetComponent<PlayerMotion>();
        health = GetComponent<HealthLogic>();
        health.OnDeath += Die; 
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
                motion.move();
                break;
            default:
                break;
        }

        updateFocusedInteractable();
        // Update interactables for new non interactable items
        /*foreach(Interactable i in interactables)
        {
            if (!i.canInteract)
            {
                removeInteractable(i);
            }
        }*/
        
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

        if (other.GetComponent<Interactable>())
        {
            if (other.GetComponent<Interactable>().canInteract)
            {
                addInteractable(other.GetComponent<Interactable>());
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            removeInteractable(other.GetComponent<Interactable>());
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
    }


    public void addInteractable(Interactable a)
    {
        if (interactables.Contains(a))
        {
            //Debug.LogWarning("Tried to add interactable twice to same player: " + a);
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
        if(interactables.Count == 0)
        {
            if(focusedInteractable != null) // We are out of range to interact, remove the selected interactable
            {
                focusedInteractable.Unfocus(this);
                focusedInteractable = null;
            }
            return;
        }
        else if(interactables.Count == 1) // If there is only one interactable object choose that object
        {
            setFocusedInteractable(interactables[0]);
            return;
        }
        if(focusedInteractable == null)
        {
            removeInteractable(focusedInteractable);
        }

        //Lifted objects take top priority
        if (liftingInteractable)
        {
            if (focusedInteractable == null)
            {
                liftingInteractable = false;
                return;
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
        if(focusedInteractable == a) { return; } // This is allready the focused interactable
        focusedInteractable?.Unfocus(this);
        a?.Focus(this);
        focusedInteractable = a;
    }

    public void Lift(GameObject a)
    {
        liftedObject = a;
        liftingInteractable = true;
        SetState(PlayerStates.Lifting);
    }

    public void Drop(GameObject a)
    {
        liftedObject = null;
        liftingInteractable = false;
        SetState(PlayerStates.Free);
        if (!a.GetComponent<Interactable>().canInteract) // Check if we can still interact with the object
        {
            removeInteractable(a.GetComponent<Interactable>());
        }
    }


    public Vector2 MoveInput { get => moveInput; }
    public Vector2 AimInput { get => aimInput; }
    public bool Interact { get => interact; }
    public bool Back { get => back; }
    public bool Select { get => select; }

    
}
