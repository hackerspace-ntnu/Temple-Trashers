using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController playerStateController = default;

    [SerializeField]
    private GameObject ui = default;

    private Transform mainCameraTransform;
    private UIWheel controllerWheel;
    private MessageUI messageUI;
    private bool buildToggle = false;

    public UIWheel ControllerWheel => controllerWheel;

    void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        controllerWheel = ui.GetComponentInChildren<UIWheel>();
    }

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        messageUI = GetComponent<MessageUI>();
    }

    public bool GetBuildToggle()
    {
        return buildToggle;
    }

    public void SetActiveUI(bool value)
    {
        ui.SetActive(value);
    }

    public void ToggleBuildMenu()
    {
        if (playerStateController.CurrentState == PlayerStates.BUILDING) { return; }
        buildToggle = !buildToggle;
        SetActiveUI(buildToggle);
        if (buildToggle)
        {
            playerStateController.SetState(PlayerStates.IN_TURRET_MENU);
        }
        else
        {
            playerStateController.SetState(PlayerStates.FREE);
        }
    }

    private void FixedUpdate()
    {
        if (ui.activeInHierarchy)
        {
            if (buildToggle)
            {
                if (playerStateController.Cancel)
                {
                    ToggleBuildMenu();
                }

                if (playerStateController.Interact)
                {
                    SetActiveUI(false);
                    StartCoroutine(WaitChange());
                    Select();
                    return;

                }
                UpdatePos();
                return;
            }

            // If the select button is no longer pressed, handle the selection
            if (!playerStateController.Select)
            {
                Select();
                return;
            }
            UpdatePos();
        }
    }

    void LateUpdate()
    {
        //Points the UI to the main camera
        Vector3 lookAtPos = transform.position + mainCameraTransform.rotation * Vector3.forward;
        ui.transform.LookAt(lookAtPos, mainCameraTransform.rotation * Vector3.up);
    }

    public TowerScriptableObject GetSelectedSegment()
    {
        return controllerWheel.GetSelectedTower();
    }

    public void Select()
    {
        TowerScriptableObject selectedSegment = GetSelectedSegment();
        if (UIManager.Singleton.ResourceAmount - selectedSegment.Cost >= 0)
        {
            UIManager.Singleton.SetResourceAmount(new ResourceInfo(-selectedSegment.Cost, gameObject));
            selectedSegment.InstantiateConstructionTower(playerStateController);
        }
        else
        {
            playerStateController.SetState(PlayerStates.FREE);
            if (UIManager.Singleton.ResourceAmount - selectedSegment.Cost < 0)
                messageUI.DisplayMessage("Missing crystals", MessageTextColor.RED);
        }
    }

    // Finds which segment of the radialUi the control stick is pointing towards
    private void UpdatePos()
    {
        // The controller points to nothing
        if (playerStateController.AimInput == Vector2.zero)
            return;

        float inputAngle = Mathf.Atan2(playerStateController.AimInput.x, playerStateController.AimInput.y) * Mathf.Rad2Deg;
        float segmentAreaDegrees = 360f / controllerWheel.GetNumSegments();
        // Update inputAngle to take into account that the origin of the icon is the middle and not the start of a segment
        inputAngle = MathUtils.NormalizeDegreeAngle(inputAngle + segmentAreaDegrees / 2f);

        // Find/set correct index
        int selectedSegmentIndex = Mathf.FloorToInt(inputAngle / segmentAreaDegrees);
        controllerWheel.SelectedSegmentIndex = selectedSegmentIndex;
    }

    private IEnumerator WaitChange()
    {
        yield return new WaitForSeconds(1f);
        buildToggle = false;
        SetActiveUI(false);
    }
}
