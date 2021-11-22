using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController state;

    public GameObject ui;

    [Tooltip("The number of degrees the menu segments are tilted.")]
    public float segmentTiltDegrees;

    private Transform mainCameraTransform;
    private InventoryManager inventory;
    private UIControllerWheel controllerWheel;
    private TowerScript selectedSegment;

    void Awake()
    {
        state = GetComponent<PlayerStateController>();
        controllerWheel = ui.GetComponentInChildren<UIControllerWheel>();
    }

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        inventory = InventoryManager.Singleton;
    }

    void LateUpdate()
    {
        //Points the UI to the main camera
        Vector3 lookAtPos = transform.position + mainCameraTransform.rotation * Vector3.forward;
        ui.transform.LookAt(lookAtPos, mainCameraTransform.rotation * Vector3.up);
    }

    public TowerScript GetSelectedSegment()
    {
        return selectedSegment;
    }

    public void Select()
    {
        //Turns on the UI
        ui.gameObject.SetActive(true);
        UpdatePos();
        //Turns off the UI if button no longer held
        if (!state.Select)
        {
            if (selectedSegment)
            {
                if (inventory.ResourceAmount - selectedSegment.cost >= 0)
                    state.SetState(PlayerStates.FREE);
                else
                {
                    inventory.ResourceAmount -= selectedSegment.cost;
                    selectedSegment.InstantiateConstructionTower(state);
                }
            }

            ui.gameObject.SetActive(false);
        }
    }

    //Finds which segment of the radialUi the control stick is pointing towards
    private void UpdatePos()
    {
        if (state.CurrentState != PlayerStates.IN_TURRET_MENU)
        {
            Debug.LogError("You seem to be in the wrong state for the UI");
            return;
        }

        // The controller points to nothing
        if (state.AimInput == Vector2.zero)
        {
            selectedSegment = null;
            ui.GetComponentInChildren<UIControllerWheel>().NormalizeSegments();
            return;
        }

        float inputAngle = 180f * Mathf.Atan2(state.AimInput.x, state.AimInput.y) / Mathf.PI;
        inputAngle = MathUtils.NormalizeDegreeAngle(inputAngle + segmentTiltDegrees);

        float segmentAreaDegrees = 360f / controllerWheel.GetNumSegments();
        int selectedSegmentIndex = Mathf.FloorToInt(inputAngle / segmentAreaDegrees);
        controllerWheel.HighlightSegment(selectedSegmentIndex);
        controllerWheel.NormalizeSegments();
        // Sets the current selected tower gameobject derived from the corresponding scriptable object
        selectedSegment = controllerWheel.GetTower(selectedSegmentIndex);
    }
}
