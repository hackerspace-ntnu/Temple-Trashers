using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController state = null;
    private GameObject ui = null;

    public GameObject[] uiSegments;
    private GameObject selectedSegment = null;



    //Sets up required components to handle input
    void Start()
    {
        state = GetComponent<PlayerStateController>();
        //ui = GetComponent <radialMenu>();
        //ui.active = true;
    }

    public void select()
    {
        updatePos();
        if (!state.Select)
        {
            //ui.active = false;
            if (selectedSegment)
            {
                // state.build(selectedSegment.getTowerObject());  
                //To be added when the ui has a refference to an actual corresponding towerObject
                state.build(selectedSegment); 
              
                return;
            }
            else
            {
                state.setStateFree();
                return;
            }
        }
    }
    
    //Finds which segment of the radialUi the control stick is pointing towards
    private void updatePos()
    {
        
        if (state.CurrentState != PlayerStateController.PlayerStates.InTurretMenu) {return;}
        if (state.AimInput != Vector2.zero) {
            float angle = Mathf.Atan2(state.AimInput.x, state.AimInput.x)/Mathf.PI;
            angle *= 180;
            angle += 90f;
            if (angle < 0)
            {
                angle += 360;
            }

            for(int i = 0; i < uiSegments.Length; i++)
            {
                if(angle > i * (360/uiSegments.Length) && angle < (i+1)* (360 / uiSegments.Length))
                {
                    selectedSegment = uiSegments[i];
                }
            }
        }
        else
        {
            selectedSegment = null;
        }

    }
}
