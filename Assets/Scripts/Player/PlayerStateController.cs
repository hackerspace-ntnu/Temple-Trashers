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
    private PlayerStates _currentState;
    private PlayerSpecificManager manager;
    private PlayerMotion motion;
    private PlayerUi ui;
    private InventoryManager inventoryManager;

    private HashSet<Interactable> interactables = new HashSet<Interactable>(); // List of interactables in range
    private Interactable _heldInteractable;
    private Interactable _focusedInteractable; // The currently focused interactable
    private GameObject _liftedObject; // Object being lifted
    public Transform inventory; // Where items are carried

    private HexGrid terrain; // Reference to the terrain
    private HexCell _targetCell;

    [SerializeField]
    private Animator anim; //Reference to animation controller of the player

    [SerializeField]
    private Transform heldItemBone;

    #region State variables for debugging

    [ReadOnly]
    public PlayerStates currentStateReadOnly;

    [ReadOnly]
    public Interactable heldInteractableReadOnly;

    [ReadOnly]
    public Interactable focusedInteractableReadOnly;

    [ReadOnly]
    public GameObject liftedObjectReadOnly;

    [ReadOnly]
    public HexCell targetCellReadOnly;

    #endregion State variables for debugging

    public PlayerStates CurrentState { get => _currentState; private set => currentStateReadOnly = _currentState = value; }

    public Interactable HeldInteractable { get => _heldInteractable; private set => heldInteractableReadOnly = _heldInteractable = value; }

    public Interactable FocusedInteractable { get => _focusedInteractable; private set => focusedInteractableReadOnly = _focusedInteractable = value; }

    public GameObject LiftedObject { get => _liftedObject; private set => liftedObjectReadOnly = _liftedObject = value; }

    public HexCell TargetCell { get => _targetCell; private set => targetCellReadOnly = _targetCell = value; }

    public delegate void PlayerStateDelegate(PlayerStates newState, PlayerStates oldState);

    public PlayerStateDelegate onPlayerStateChange; //To allow other components to subscribe to stateChange events

    private static readonly int LiftingAnimatorParam = Animator.StringToHash("Lifting");
    private static readonly int PlanningAnimatorParam = Animator.StringToHash("Planning");

    void Start()
    {
        motion = GetComponent<PlayerMotion>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Die;
        ui = GetComponent<PlayerUi>();
        terrain = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        inventoryManager = InventoryManager.Singleton;

        CurrentState = PlayerStates.FREE;
    }

    void FixedUpdate()
    {
        switch (CurrentState)
        {
            case PlayerStates.IN_ANIMATION:
                break;
            case PlayerStates.LIFTING:
                UpdateFocusedInteractable();
                motion.Move();
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                UpdateFocusedInteractable();
                if (Select)
                    SetState(PlayerStates.IN_TURRET_MENU);
                motion.Move();
                break;
            case PlayerStates.IN_TURRET_MENU:
                ui.Select();
                motion.Move();
                break;
            case PlayerStates.BUILDING:
                UpdateFocusedInteractable();
                motion.Move();
                TargetCell = terrain.GetCell(transform.position + HexMetrics.outerRadius * 2f * transform.forward);
                FocusedInteractable.GetComponent<TurretPrefabConstruction>().FocusCell(TargetCell);
                if (Cancel)
                {
                    //Refund turret
                    inventoryManager.ResourceAmount += ui.GetSelectedCost();

                    RemoveInteractable(HeldInteractable);
                    Destroy(HeldInteractable.gameObject);
                    HeldInteractable = null;
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

    private void Die(HealthLogic.DamageInstance dmg)
    {
        // Drop anything we are carrying
        if (LiftedObject != null)
            LiftedObject.GetComponent<Interactable>().Interact(this);

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
                anim.SetBool(LiftingAnimatorParam, false);
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.BUILDING:
                anim.SetBool(LiftingAnimatorParam, false);
                break;
            case PlayerStates.IN_TURRET_MENU:
                anim.SetBool(PlanningAnimatorParam, false);
                break;
            default:
                break;
        }

        //Effects when changing to state
        switch (state)
        {
            case PlayerStates.LIFTING:
                anim.SetBool(LiftingAnimatorParam, true);
                break;
            case PlayerStates.DEAD:
                if (HeldInteractable)
                {
                    //Refund turret
                    inventoryManager.ResourceAmount += ui.GetSelectedCost();

                    RemoveInteractable(HeldInteractable);
                    Destroy(HeldInteractable.gameObject);
                    HeldInteractable = null;
                }

                SetFocusedInteractable(null);
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.IN_TURRET_MENU:
                SetFocusedInteractable(null);
                anim.SetBool(PlanningAnimatorParam, true);
                break;
            case PlayerStates.BUILDING:
                anim.SetBool(LiftingAnimatorParam, true);
                break;
            case PlayerStates.IN_ANIMATION:
                SetFocusedInteractable(null);
                break;
            default:
                break;
        }

        CurrentState = state;
    }

    // Gets called when interact button is pressed
    private void OnInteract()
    {
        if (FocusedInteractable != null)
            FocusedInteractable.Interact(this); // interact with the current target
        if (FocusedInteractable != null && !FocusedInteractable.canInteract)
            RemoveInteractable(FocusedInteractable);

        if (CurrentState == PlayerStates.BUILDING)
        {
            // Build the turret we are holding
            FocusedInteractable.GetComponent<TurretPrefabConstruction>().Construct(TargetCell);
            Drop(FocusedInteractable.gameObject);
            RemoveInteractable(FocusedInteractable);
            SetState(PlayerStates.FREE);
        }
    }

    // Called when the "Move tower" button is pressed
    private void OnMoveTower()
    {
        if (!(FocusedInteractable is TowerLogic tower))
            return;

        tower.TowerScript.InstantiateConstructionTower(this);

        RemoveInteractable(tower);
        Destroy(tower.gameObject);
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
            if (FocusedInteractable != null) // The object we were focusing is no longer focusable
            {
                FocusedInteractable.Unfocus(this);
                FocusedInteractable = null;
            }

            // Otherwise do nothing
            return;
        } else if (interactables.Count == 1) // If there is only one interactable object select that object
        {
            SetFocusedInteractable(interactables.Single());
            return;
        } else if (FocusedInteractable == null && HeldInteractable == null) // Wait until we have at least one object to interact with
            return;

        // Lifted objects are top priority
        if (HeldInteractable != null && HeldInteractable != FocusedInteractable)
        {
            SetFocusedInteractable(HeldInteractable);
            return;
        }

        // Otherwise get closest interactable
        Interactable closest = FocusedInteractable;
        float dist = (FocusedInteractable.transform.position - transform.position).sqrMagnitude;
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
        if (FocusedInteractable == interactable)
        {
            // This is already the focused interactable
            return;
        }

        if (FocusedInteractable)
            FocusedInteractable.Unfocus(this);
        if (interactable)
            interactable.Focus(this);
        FocusedInteractable = interactable;
    }

    public void Lift(GameObject obj)
    {
        LiftedObject = obj;
        SetState(PlayerStates.LIFTING);
        obj.transform.SetParent(heldItemBone);
        obj.transform.localPosition = Vector3.zero;
    }

    public void PrepareTurret(Interactable turret)
    {
        AddInteractable(turret);
        HeldInteractable = turret;
        SetFocusedInteractable(turret);
    }

    public void Drop(GameObject obj)
    {
        LiftedObject = null;
        obj.transform.parent = null;
        SetState(PlayerStates.FREE);
    }
}
