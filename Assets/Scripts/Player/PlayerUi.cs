using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController state = null;
    public GameObject ui;
    public int uiSegmentAmount = 4;
    public float segmentTiltDegrees;
    private Transform mainCameraTransform;


    private GameObject selectedSegment = null;



    //Sets up required components to handle input
    void Start()
    {
        state = GetComponent<PlayerStateController>();
        mainCameraTransform = Camera.main.transform;

    }

    public GameObject GetSelectedSegment()
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
            ui.gameObject.SetActive(false);
            if (GetSelectedSegment())
            {
                GameObject spawnedTower = Instantiate(GetSelectedSegment());
                state.Lift(spawnedTower);
                state.SetState(PlayerStateController.PlayerStates.Building);
            }
            else
            {
                state.SetState(PlayerStateController.PlayerStates.Free);
            }
        }
        //Points the UI to the main camera
        ui.transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
            mainCameraTransform.rotation * Vector3.up);
    }
    
    //Finds which segment of the radialUi the control stick is pointing towards
    private void UpdatePos()
    {
        if (state.CurrentState != PlayerStateController.PlayerStates.InTurretMenu) { Debug.Log("You seem to be in the wrong state for the UI"); return;}
        if (state.AimInput != Vector2.zero) {
            float angle = Mathf.Atan2(state.AimInput.x, state.AimInput.y)/Mathf.PI;
            angle *= 180; 

            //Degree-tilt on segments to accomadate for tilted menus.
            angle += segmentTiltDegrees;

            //absoluteValue of angle
            if (angle < 0)
            {
                angle += 360;
            }
            
            for(int i = 0; i < uiSegmentAmount; i++)
            {
                if(angle > i * (360/uiSegmentAmount) && angle < (i+1)* (360 / uiSegmentAmount))
                {
                    //Sets all ui segments to their normal non-highlighted texture
                    ui.GetComponentInChildren<UIController>().NormalizeSegments();
                    //Highlights the selected segment
                    ui.GetComponentInChildren<UIController>().HighlightSegment(i);
                    //Sets the current selected tower gameobject derived from the corresponding scriptable object
                    selectedSegment = ui.GetComponentInChildren<UIController>().GetTower(i);
                }
            }

        }
        //The controller points to nothing
        else
        {
            selectedSegment = null;
            ui.GetComponentInChildren<UIController>().NormalizeSegments();
        }

    }
}
