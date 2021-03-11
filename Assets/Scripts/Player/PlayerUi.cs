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
        
        //uiSegments =
    }

    public GameObject getSelectedSegment()
    {
        return selectedSegment;
    }

    public void select()
    {
        ui.gameObject.SetActive(true);
        updatePos();
        ui.gameObject.SetActive(true);
        if (!state.Select)
        {
            ui.gameObject.SetActive(false);
        }
    }
    
    //Finds which segment of the radialUi the control stick is pointing towards
    private void updatePos()
    {
        if (state.CurrentState != PlayerStateController.PlayerStates.InTurretMenu) { Debug.Log("Er ikke i turretMenu, så  skyter tilbake"); return;}
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
                    selectedSegment = ui.GetComponent<UIController>().getTower(i);
                }
            }

        }
        else
        {
            selectedSegment = null;
        }

    }
}
