using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    //private play
    private PlayerInput playerInput;

    public Vector2 MoveInput { get; private set; } = Vector2.zero;

    private void MoveInput_Performed(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>().magnitude > 0.1f ? ctx.ReadValue<Vector2>() : Vector2.zero;
    private void MoveInput_Canceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    private void MoveInputer(InputAction.CallbackContext ctx) => OnMove();
    private void MoveInput_Interacted(InputAction.CallbackContext ctx) => Select();

        void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += MoveInput_Performed;
        playerInput.actions["Move"].performed += MoveInputer;
        playerInput.actions["Move"].canceled += MoveInput_Canceled;
        playerInput.actions["Interact"].performed += MoveInput_Interacted;
    }


    private void OnMove()

    {
        if (MoveInput.magnitude > 0.1f)
        {
            if (MoveInput.y > 0)
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

    private void OnDestroy()
    {
        playerInput.actions["Move"].performed -= MoveInputer;
        playerInput.actions["Move"].performed -= MoveInput_Performed;
        playerInput.actions["Move"].performed -= MoveInput_Canceled;
        playerInput.actions["Interact"].performed -= MoveInput_Interacted;
    }

}
