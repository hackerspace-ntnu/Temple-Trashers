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
/// The input-related code is in <c>PlayerStateController_Input.cs</c>.
/// </summary>
public partial class PlayerStateController : MonoBehaviour
{
    private HealthLogic health; // Reference to the health script

    [TextLabel(greyedOut = true), SerializeField]
    private PlayerStates _currentState = PlayerStates.FREE;

    private PlayerSpecificManager manager;
    private PlayerMotion motion;
    private PlayerUi ui;

    private HashSet<Interactable> interactables = new HashSet<Interactable>(); // List of interactables in range

    [ReadOnly, SerializeField]
    private Interactable heldInteractable;

    [ReadOnly, SerializeField]
    private Interactable focusedInteractable; // The currently focused interactable

    [ReadOnly, SerializeField]
    private GameObject liftedObject; // Object being lifted

    public Transform inventory; // Where items are carried

    [ReadOnly, SerializeField]
    private HexCell targetCell;

    [SerializeField]
    private Animator anim = default; //Reference to animation controller of the player

    [SerializeField]
    private Transform heldItemBone = default;

    public Transform enemyViewFocus;

    private MessageUI messageUI;

    [SerializeField]
    private SkinnedMeshRenderer toggleMaterial = default;

    public PlayerStates CurrentState { get => _currentState; private set => _currentState = value; }

    public HexCell TargetCell => targetCell;

    public delegate void PlayerStateDelegate(PlayerStates newState, PlayerStates oldState);

    public PlayerStateDelegate onPlayerStateChange; //To allow other components to subscribe to stateChange events

    private static readonly int liftingAnimatorParam = Animator.StringToHash("Lifting");
    private static readonly int planningAnimatorParam = Animator.StringToHash("Planning");
    private static readonly int borderScaleShaderProperty = Shader.PropertyToID("BorderScale");

    public bool deathCooldownOver = false;
    private float deathCooldown = 2f;
    private PlayerRagdollController ragdollCont;

    private Transform deadplayerVFX;

    void Awake()
    {
        motion = GetComponent<PlayerMotion>();
        health = GetComponent<HealthLogic>();
        health.onDeath += Die;

        ui = GetComponent<PlayerUi>();
        messageUI = GetComponent<MessageUI>();
        uiInputController = GetComponent<UIInputController>();
        ragdollCont = GetComponent<PlayerRagdollController>();
}

    void OnDestroy()
    {
        health.onDeath -= Die;

        OnDestroy_Input();
    }

    private void Die(DamageInfo dmg)
    {
        // Drop anything we are carrying
        if (liftedObject)
            liftedObject.GetComponent<Interactable>().Interact(this);

        SetState(PlayerStates.DEAD);
        manager.StartRespawnPlayer(deathCooldown);

        CameraFocusController.Singleton.RemoveFocusObject(transform);
        GetComponent<PlayerRagdollController>()?.Ragdoll(dmg);
        anim.enabled = false;

        //Makes sure respawn blink is disabled before ragdoll is made.
        DisableOutline();
    }

    void Start()
    {
        CurrentState = PlayerStates.FREE;

        StartCoroutine(SpawnEffectTimer());
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
                MoveBackToSpawn();
                break;
            case PlayerStates.FREE:
                UpdateFocusedInteractable();
                motion.Move();

                if (Select)
                    SetState(PlayerStates.IN_TURRET_MENU);
                break;
            case PlayerStates.IN_TURRET_MENU:
                motion.Move();
                break;
            case PlayerStates.BUILDING:
                UpdateFocusedInteractable();
                motion.Move();
                UpdateConstructionTower();
                if (Cancel)
                {
                    SellTower();
                    SetState(PlayerStates.FREE);
                }

                if (Interact)
                    SetState(PlayerStates.FREE);

                break;
        }
    }

    public void ReturnPlayerToSpawn()
    {
        deathCooldownOver = true;
        deadplayerVFX = BaseController.Singleton.OnPlayerDeath(this.transform);
    }

    private void MoveBackToSpawn()
    {
        if(deathCooldownOver)
        {
            foreach (var t in ragdollCont.initialForceTarget.transform.parent.GetComponentsInChildren<Transform>())
            {
                t.localPosition = Vector3.Lerp(t.localPosition, Vector3.zero, Time.deltaTime);
            }

            ragdollCont.initialForceTarget.transform.parent.position = Vector3.Lerp(ragdollCont.initialForceTarget.transform.parent.position, manager.spawnPoint, Time.deltaTime);
            
            transform.position = Vector3.Lerp(transform.position, manager.spawnPoint, Time.deltaTime);
            anim.enabled = true;

            if (Vector3.Distance(ragdollCont.transform.position, manager.spawnPoint) < 0.1f)
            {
                manager.RespawnPlayer();// Respawn a new play
                Destroy(deadplayerVFX.gameObject); // Destroy lightning
                Destroy(gameObject);    // Destroy Game object
            }
        }
    }

    private void UpdateConstructionTower()
    {
        TurretPrefabConstruction turretConstruction = focusedInteractable.GetComponent<TurretPrefabConstruction>();

        HexCell newTargetCell = HexGrid.Singleton.GetCell(transform.position + HexGrid.OUTER_RADIUS * 2f * transform.forward);
        if (newTargetCell != targetCell)
        {
            targetCell = newTargetCell;
            turretConstruction.FocusCell(targetCell);
        }
        turretConstruction.RotateFacing(transform.forward);
    }

    void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();
        if (interactable && interactable.canInteract)
            AddInteractable(interactable);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
            RemoveInteractable(other.GetComponentInParent<Interactable>());
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
                anim.SetBool(liftingAnimatorParam, false);
                break;
            case PlayerStates.DEAD:
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.BUILDING:
                anim.SetBool(liftingAnimatorParam, false);
                break;
            case PlayerStates.IN_TURRET_MENU:
                anim.SetBool(planningAnimatorParam, false);
                ui.SetActiveUI(false);
                break;
        }

        //Effects when changing to state
        switch (state)
        {
            case PlayerStates.LIFTING:
                anim.SetBool(liftingAnimatorParam, true);
                break;
            case PlayerStates.DEAD:
                if (heldInteractable)
                {
                    //Refund turret
                    SellTower();
                }
                //Slowmo kill effect
                StartCoroutine(SlowMo());
                SetFocusedInteractable(null);
                break;
            case PlayerStates.FREE:
                break;
            case PlayerStates.IN_TURRET_MENU:
                SetFocusedInteractable(null);
                anim.SetBool(planningAnimatorParam, true);
                ui.SetActiveUI(true);
                break;
            case PlayerStates.BUILDING:
                anim.SetBool(liftingAnimatorParam, true);
                break;
            case PlayerStates.IN_ANIMATION:
                SetFocusedInteractable(null);
                break;
        }

        CurrentState = state;
    }

    // Gets called when interact button is pressed
    private void OnInteract()
    {
        if (!focusedInteractable)
            return;

        if (!focusedInteractable.canInteract)
        {
            RemoveInteractable(focusedInteractable);
            return;
        }

        focusedInteractable.Interact(this); // interact with the current target

        if (CurrentState == PlayerStates.BUILDING && targetCell.CanPlaceTowerOnCell)
        {
            // Build the turret we are holding
            focusedInteractable.GetComponent<TurretPrefabConstruction>().Construct(targetCell, transform.forward);
            messageUI.DisplayMessage($"-{focusedInteractable.GetComponent<TurretPrefabConstruction>().TowerScriptableObject.Cost}", MessageTextColor.RED);
            Drop(focusedInteractable.gameObject);
            RemoveInteractable(focusedInteractable);
            SetState(PlayerStates.FREE);
        }
    }

    // Called when the "Move tower" button is pressed
    private void OnMoveTower()
    {
        if (!(focusedInteractable is TowerLogic tower))
            return;

        tower.TowerScriptableObject.InstantiateConstructionTower(this);

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
        // Lifted objects are top priority
        if (heldInteractable)
        {
            SetFocusedInteractable(heldInteractable);
            return;
        }

        if (interactables.Count == 0)
        {
            if (focusedInteractable) // The object we were focusing is no longer focusable
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
        } else if (!focusedInteractable && !heldInteractable) // Wait until we have at least one object to interact with
            return;

        // Otherwise get closest interactable
        Interactable closest = focusedInteractable;
        float closestDistSqr = (focusedInteractable.transform.position - transform.position).sqrMagnitude;
        // Remove non-existent interactables (e.g. if they were destroyed)
        interactables.RemoveWhere(interactable => !interactable);
        foreach (Interactable interactable in interactables)
        {
            float distSqr = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distSqr < closestDistSqr)
            {
                closestDistSqr = distSqr;
                closest = interactable;
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

    private void SellTower()
    {
        if (!(heldInteractable is TurretPrefabConstruction tower))
            return;

        UIManager.Singleton.SetResourceAmount(new ResourceInfo(tower.TowerScriptableObject.Cost, gameObject));
        messageUI.DisplayMessage($"+{tower.TowerScriptableObject.Cost}", MessageTextColor.GREEN);

        RemoveInteractable(tower);
        Destroy(tower.gameObject);
    }

    public void Lift(GameObject obj)
    {
        liftedObject = obj;
        SetState(PlayerStates.LIFTING);
        obj.transform.SetParent(heldItemBone);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }

    public void Drop(GameObject obj)
    {
        liftedObject = null;
        obj.transform.parent = null;
        SetState(PlayerStates.FREE);
    }

    public void PrepareTurret(Interactable turret)
    {
        AddInteractable(turret);
        heldInteractable = turret;
        SetFocusedInteractable(turret);
        UpdateConstructionTower();
    }

    private void ResetOutline()
    {
        toggleMaterial.materials[1].SetFloat(borderScaleShaderProperty, 1.04f);
    }

    public void DisableOutline()
    {
        toggleMaterial.materials[1].SetFloat(borderScaleShaderProperty, 0.01f);
    }

    private IEnumerator SpawnEffectTimer()
    {
        ResetOutline();
        float maxHealthPrev = health.maxHealth;
        //Set health to an arbitrarly high amount
        health.maxHealth = 10000;
        health.health= 10000;
        yield return new WaitForSeconds(3f);
        //Reset health
        health.maxHealth = maxHealthPrev;
        health.health = maxHealthPrev;
        DisableOutline();
    }

    private IEnumerator SlowMo()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
