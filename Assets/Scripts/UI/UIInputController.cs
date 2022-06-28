using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    //private play
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //playerInput.actions["Move"].performed += SetRelevantButton();
        //playerInput.actions["Interact"].performed += Select();
    }

    private void OnMove()
    {
        
    }

    private void SetRelevantButton(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<Vector2>().magnitude > 0.1f)
        {
            if (ctx.ReadValue<Vector2>().y > 0)
            {
                ControllerButtonNavigator.currentButton.buttonUp.SetCurrentButton();
            }
            else
            {
                ControllerButtonNavigator.currentButton.buttonDown.SetCurrentButton();
            }
        }
    }

    private void Select()
    {
        ControllerButtonNavigator.currentButton.PressButton();
    }
    


}
