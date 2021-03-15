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


    private GameObject selectedSegment = null;



    //Sets up required components to handle input
    void Start()
    {
        state = GetComponent<PlayerStateController>();
    }

    public GameObject getSelectedSegment()
    {
        return selectedSegment;
    }

    public void select()
    {
        //Turns on the UI
        ui.gameObject.SetActive(true);
        updatePos();
        //Turns off the UI if button no longer held
        if (!state.Select)
        {
            ui.gameObject.SetActive(false);
        }
        //Points the UI to the main camera
        ui.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
    
    //Finds which segment of the radialUi the control stick is pointing towards
    private void updatePos()
    {
        if (state.CurrentState != PlayerStateController.PlayerStates.InTurretMenu) { Debug.Log("You seem to be in the wrong state for the UI"); return;}
        if (state.AimInput != Vector2.zero) {
            float angle = Mathf.Atan2(state.AimInput.x, state.AimInput.y)/Mathf.PI;
            angle *= 180; 
            angle += 45f;
            if (angle < 0)
            {
                angle += 360;
            }
            
            for(int i = 0; i < uiSegmentAmount; i++)
            {
                if(angle > i * (360/uiSegmentAmount) && angle < (i+1)* (360 / uiSegmentAmount))
                {
                    //Sets all ui segments to their normal non-highlighted texture
                    ui.GetComponentInChildren<UIController>().normalizeSegments();
                    //Highlights the selected segment
                    ui.GetComponentInChildren<UIController>().highlightSegment(i);
                    //Sets the current selected tower gameobject derived from the corresponding scriptable object
                    selectedSegment = ui.GetComponentInChildren<UIController>().getTower(i);
                }
            }

        }
        //The controller points to nothing
        else
        {
            selectedSegment = null;
            ui.GetComponentInChildren<UIController>().normalizeSegments();
        }

    }
}
