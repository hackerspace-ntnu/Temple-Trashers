using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController state;

    public GameObject ui;
    public int uiSegmentAmount = 4;
    public float segmentTiltDegrees;
    private Transform mainCameraTransform;

    private InventoryManager inventory;
    private TowerScript selectedSegment;

    //Sets up required components to handle input
    void Start()
    {
        state = GetComponent<PlayerStateController>();
        mainCameraTransform = Camera.main.transform;
        inventory = InventoryManager.Singleton;
    }

    public GameObject GetSelectedSegment()
    {
        return selectedSegment.tower;
    }

    public int GetSelectedCost()
    {
        return selectedSegment.cost;
    }

    public void Select()
    {
        //Turns on the UI
        ui.gameObject.SetActive(true);
        UpdatePos();
        //Turns off the UI if button no longer held
        if (!state.Select)
        {
            ui.gameObject.SetActive(false);
            if (selectedSegment)
            {
                inventory.ResourceAmount -= GetSelectedCost();
                if (inventory.ResourceAmount >= 0)
                {
                    GameObject spawnedTower = Instantiate(GetSelectedSegment());
                    state.PrepareTurret(spawnedTower.GetComponent<Interactable>());
                    state.SetState(PlayerStates.BUILDING);
                }
            } else
            {
                state.SetState(PlayerStates.FREE);
            }
        }
    }

    private void LateUpdate()
    {
        //Points the UI to the main camera
        Vector3 lookAtPos = transform.position + mainCameraTransform.rotation * Vector3.forward;
        ui.transform.LookAt(lookAtPos, mainCameraTransform.rotation * Vector3.up);
    }

    //Finds which segment of the radialUi the control stick is pointing towards
    private void UpdatePos()
    {
        if (state.CurrentState != PlayerStates.IN_TURRET_MENU)
        {
            Debug.LogError("You seem to be in the wrong state for the UI");
            return;
        }

        if (state.AimInput != Vector2.zero)
        {
            float angle = 180 * Mathf.Atan2(state.AimInput.x, state.AimInput.y) / Mathf.PI;

            //Degree-tilt on segments to accomadate for tilted menus.
            angle += segmentTiltDegrees;

            //absoluteValue of angle
            if (angle < 0)
                angle += 360;

            float segmentAreaDegrees = 360f / uiSegmentAmount;
            for (int i = 0; i < uiSegmentAmount; i++)
            {
                if (angle >= i * segmentAreaDegrees
                    && angle < (i + 1) * segmentAreaDegrees)
                {
                    //Sets all ui segments to their normal non-highlighted texture
                    ui.GetComponentInChildren<UIControllerWheel>().NormalizeSegments();
                    //Highlights the selected segment
                    ui.GetComponentInChildren<UIControllerWheel>().HighlightSegment(i);
                    //Sets the current selected tower gameobject derived from the corresponding scriptable object
                    selectedSegment = ui.GetComponentInChildren<UIControllerWheel>().GetTower(i);
                }
            }
        }
        //The controller points to nothing
        else
        {
            selectedSegment = null;
            ui.GetComponentInChildren<UIControllerWheel>().NormalizeSegments();
        }
    }
}
