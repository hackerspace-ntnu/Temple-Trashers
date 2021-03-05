using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private PlayerStateController state = null;
    private GameObject ui = null;
    private quadrants selectedElement = quadrants.None;


    //the following "quadrants" enum represents the corresponding radial section of the radialUi
    public enum quadrants
    {
        NorthWest,
        NorthEast,
        SouthEast,
        SouthWest,
        None
    }


    //Sets up required components to handle input
    void Start()
    {
        state = GetComponent<PlayerStateController>();
        //ui = GetComponent <radialMenu>();
    }

    public void select()
    {
        updatePos();
    }
    
    private void updatePos()
    {
        /*
        if (state.CurrentState != PlayerStateController.PlayerStates.InTurretMenu) {return;}
        switch (state)
        {
            case (state.AimInput == Vector2.zero):
                selectedElement = quadrants.None;
                break;

        }*/

    }
}
