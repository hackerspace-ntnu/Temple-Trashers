using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerStates
{
    IN_ANIMATION,
    LIFTING,
    DEAD,
    FREE,
    BUILDING,
    IN_TURRET_MENU,
}

/// <summary>
/// The input-related code is in `PlayerStateController_Input.cs`.
/// </summary>
public partial class PlayerStateController : MonoBehaviour
{
    private HealthLogic health; // Reference to the health script
    private PlayerSpecificManager manager;
    private PlayerMotion motion;
    private PlayerUi ui;
    private InventoryManager inventoryManager;

    private HashSet<Interactable> interactables = new HashSet<Interactable>(); // List of interactables in range
    private Interactable heldInteractable;
    private Interactable focusedInteractable; // The currently focused interactable
    private GameObject liftedObject; // Object being lifted
    public Transform inventory; // Where items are carried

    private HexGrid terrain; // Reference to the terrain
    private HexCell targetCell;

    [SerializeField]
    private Animator anim; //Reference to animation controller of the player

    [SerializeField]
    private Transform heldItemBone;

    public PlayerStates CurrentState { get; private set; } = PlayerStates.FREE;

    public delegate void PlayerStateDelegate(PlayerStates newState, PlayerStates oldState);

    public PlayerStateDelegate onPlayerStateChange; //To allow other components to subscribe to stateChange events

    void Start()
    {
        motion = GetComponent<PlayerMotion>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Die;
        ui = GetComponent<PlayerUi>();
        terrain = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        inventoryManager = InventoryManager.Singleton;
    }

    void FixedUpdate()
    {
        UpdateFocusedInteractable();
        switch (CurrentState)
        {
            case PlayerStates.IN_ANIMATION:
                break;
            case PlayerStates.LIFTING:
                //global
                motion.Move();
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                if (Select)
                    SetState(PlayerStates.IN_TURRET_MENU);
                //global
                motion.Move();
                break;
            case PlayerStates.IN_TURRET_MENU:
                //global
                ui.Select();
                motion.Move();
                //Lift(spawnedTower);
                break;
            case PlayerStates.BUILDING:
                //global
                motion.Move();
                targetCell = terrain.GetCell(transform.position + HexMetrics.outerRadius * 2f * transform.forward);
                focusedInteractable.GetComponent<TurretPrefabConstruction>().FocusCell(targetCell);
                //case specific
                if (Cancel)
                {
                    //Refund turret
                    inventoryManager.ResourceAmount += ui.GetSelectedCost();

                    RemoveInteractable(heldInteractable);
                    Destroy(heldInteractable.gameObject);
                    heldInteractable = null;
                    SetState(PlayerStates.FREE);
                }

                if (Interact)
                    SetState(PlayerStates.FREE);

                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();
        if (interactable != null && interactable.canInteract)
            AddInteractable(interactable);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
            RemoveInteractable(other.GetComponentInParent<Interactable>());
    }

    private void Die()
    {
        // Drop anything we are carrying
        if (liftedObject != null)
            liftedObject.GetComponent<Interactable>().Interact(this);

        SetFocusedInteractable(null);
        SetState(PlayerStates.DEAD);
        manager.RespawnPlayer(1f);

        CameraFocusController.Singleton.RemoveFocusObject(transform);
        Destroy(gameObject);
    }

    public void SetState(PlayerStates state)
    {
        if (state == CurrentState)
            return;

        onPlayerStateChange?.Invoke(state, CurrentState); //In case any script wants to subscribe to state change events
        //Cleanup from previous state
        switch (CurrentState)
        {
            case PlayerStates.IN_ANIMATION:
                break;
            case PlayerStates.LIFTING:
                //TODO: Remove the current item the player is lifting
                anim.SetBool("Lifting", false);
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.BUILDING:
                anim.SetBool("Lifting", false);
                break;
            case PlayerStates.IN_TURRET_MENU:
                anim.SetBool("Planning", false);
                break;
            default:
                break;
        }

        //Effects when changing to state
        switch (state)
        {
            case PlayerStates.LIFTING:
                anim.SetBool("Lifting", true);
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.IN_TURRET_MENU:
                anim.SetBool("Planning", true);
                break;
            case PlayerStates.BUILDING:
                anim.SetBool("Lifting", true);
                //AddInteractable(liftedObject.GetComponent<Interactable>());
                break;
            case PlayerStates.IN_ANIMATION:
                break;
            default:
                break;
        }

        CurrentState = state;
    }

    // Gets called when interact button is pressed
    private void OnInteract()
    {
        if (focusedInteractable != null)
            focusedInteractable.Interact(this); // interact with the current target
        if (focusedInteractable != null && !focusedInteractable.canInteract)
            RemoveInteractable(focusedInteractable);

        if (CurrentState == PlayerStates.BUILDING)
        {
            // Build the turret we are holding
            focusedInteractable.GetComponent<TurretPrefabConstruction>().Construct(targetCell);
            Drop(focusedInteractable.gameObject);
            RemoveInteractable(focusedInteractable);
            SetState(PlayerStates.FREE);
        }
    }

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    // Update the focused interactable based on distance from player
    private void UpdateFocusedInteractable()
    {
        if (interactables.Count == 0)
        {
            if (focusedInteractable != null) // The object we were focusing is no longer focusable
            {
                focusedInteractable.Unfocus(this);
                focusedInteractable = null;
            }

            // Otherwise do nothing
            return;
        } else if (interactables.Count == 1) // If there is only one interactable object select that object
        {
            SetFocusedInteractable(interactables.Single());
            return;
        } else if (focusedInteractable == null && heldInteractable == null) // Wait until we have at least one object to interact with
            return;

        // Lifted objects are top priority
        if (heldInteractable != null && heldInteractable != focusedInteractable)
        {
            SetFocusedInteractable(heldInteractable);
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

    private void SetFocusedInteractable(Interactable interactable)
    {
        if (focusedInteractable == interactable)
        {
            // This is already the focused interactable
            return;
        }

        if (focusedInteractable)
            focusedInteractable.Unfocus(this);
        if (interactable)
            interactable.Focus(this);
        focusedInteractable = interactable;
    }

    public void Lift(GameObject obj)
    {
        liftedObject = obj;
        SetState(PlayerStates.LIFTING);
        obj.transform.SetParent(heldItemBone);
        obj.transform.localPosition = Vector3.zero;
    }

    public void PrepareTurret(Interactable turret)
    {
        heldInteractable = turret;
        AddInteractable(turret);
    }

    public void Drop(GameObject obj)
    {
        liftedObject = null;
        obj.transform.parent = null;
        SetState(PlayerStates.FREE);
        // Check if we can still interact with the object
        //if (!obj.GetComponent<Interactable>().canInteract)
        //    RemoveInteractable(obj.GetComponent<Interactable>());
    }
}
