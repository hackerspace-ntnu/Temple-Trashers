using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIInputController : MonoBehaviour
{
    private PlayerInput playerInput;

    private float snapshot;

    public Vector2 MoveInput { get; private set; } = Vector2.zero;

    private void MoveInput_Performed(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>().magnitude > 0.1f ? ctx.ReadValue<Vector2>() : Vector2.zero;
    private void MoveInput_Canceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    private void MoveInputer(InputAction.CallbackContext ctx) => OnMove();
    private void MoveInput_Interacted(InputAction.CallbackContext ctx) => Select();


    public void setUIInputController(PlayerInput playerInput) 
    {
        this.playerInput = playerInput;
    }

        void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        ListenersAdd();   
    }


    private void OnMove()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            Debug.Log("OnMove called in main_menu");
            if (Time.fixedTime - snapshot > 0.2f)
            {
                snapshot = Time.fixedTime;

                DetermineDirection();
            }
            Debug.Log(ControllerButtonNavigator.currentButton);
        }
        else if (PauseManager.Singleton.IsPaused || BaseController.Singleton.isGameOver)
        {
            DetermineDirection();
        }
    }

    private void DetermineDirection()
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
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            ControllerButtonNavigator.currentButton.PressButton();
        }
        else if (PauseManager.Singleton.IsPaused || BaseController.Singleton.isGameOver) 
        { 
            ControllerButtonNavigator.currentButton.PressButton();
        }
    }

    public void ListenersAdd()
    {

        playerInput.actions["Move"].performed += MoveInput_Performed;
        playerInput.actions["Move"].performed += MoveInputer;
        playerInput.actions["Move"].canceled += MoveInput_Canceled;
        playerInput.actions["Interact"].performed += MoveInput_Interacted;

    }

    public void ListenersRemove()
    {
        playerInput.actions["Move"].performed -= MoveInputer;
        playerInput.actions["Move"].performed -= MoveInput_Performed;
        playerInput.actions["Move"].performed -= MoveInput_Canceled;
        playerInput.actions["Interact"].performed -= MoveInput_Interacted;
    }

}
