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
    private InventoryManager inventory;
    private UIWheel controllerWheel;
    private MessageUI messageUI;

    public UIWheel ControllerWheel => controllerWheel;

    void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        controllerWheel = ui.GetComponentInChildren<UIWheel>();
    }

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        inventory = InventoryManager.Singleton;
        messageUI = GetComponent<MessageUI>();
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
        //Turns on the UI
        ui.gameObject.SetActive(true);
        UpdatePos();
        //Turns off the UI if button no longer held
        if (!playerStateController.Select)
        {
            TowerScriptableObject selectedSegment = GetSelectedSegment();
            if (selectedSegment
                && inventory.ResourceAmount - selectedSegment.Cost >= 0)
            {
                inventory.ResourceAmount -= selectedSegment.Cost;
                selectedSegment.InstantiateConstructionTower(playerStateController);
                playerStateController.SetState(PlayerStates.BUILDING);
            } else
            {
                playerStateController.SetState(PlayerStates.FREE);
                if (inventory.ResourceAmount - selectedSegment.Cost < 0)
                    messageUI.DisplayMessage("Missing crystals", MessageTextColor.RED);
            }

            ui.gameObject.SetActive(false);
        }
    }

    //Finds which segment of the radialUi the control stick is pointing towards
    private void UpdatePos()
    {
        if (playerStateController.CurrentState != PlayerStates.IN_TURRET_MENU)
        {
            Debug.LogError("You seem to be in the wrong state for the UI");
            return;
        }

        // The controller points to nothing
        if (playerStateController.AimInput == Vector2.zero)
            return;

        float inputAngle = 180f * Mathf.Atan2(playerStateController.AimInput.x, playerStateController.AimInput.y) / Mathf.PI;
        if (inputAngle < 0f)
            inputAngle += 360f;

        float segmentAreaDegrees = 360f / controllerWheel.GetNumSegments();

        //Update inputAngle to take into account that the origin of the icon is the middle and not the start of a segment
        inputAngle = MathUtils.NormalizeDegreeAngle(inputAngle + segmentAreaDegrees / 2f);

        //Find/set correct index
        int selectedSegmentIndex = Mathf.FloorToInt(inputAngle / segmentAreaDegrees);
        controllerWheel.SelectedSegmentIndex = selectedSegmentIndex;
    }
}
