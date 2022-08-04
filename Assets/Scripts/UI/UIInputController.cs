using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class UIInputController : MonoBehaviour
{
    [SerializeField]
    private float buttonChangeDelay = 0.2f;

    private PlayerInput playerInput;

    private float snapshot;

    public Vector2 MoveInput { get; private set; } = Vector2.zero;

    private void MoveInput_Performed(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>().magnitude > 0.1f
            ? ctx.ReadValue<Vector2>()
            : Vector2.zero;
        OnMove();
    }

    private void MoveInput_Canceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    private void InteractInput_Performed(InputAction.CallbackContext ctx) => Select();

     void Start()
     {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            playerInput = GetComponent<PlayerInput>();
            AddListeners();
        }
     }

    public void SetUpInput(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
        AddListeners();
    }

    void OnDestroy()
    {
        if (playerInput)
            RemoveListeners();
    }

    private void AddListeners()
    {
        playerInput.actions["Move"].performed += MoveInput_Performed;
        playerInput.actions["Move"].canceled += MoveInput_Canceled;
        playerInput.actions["Interact"].performed += InteractInput_Performed;
    }

    private void RemoveListeners()
    {
        playerInput.actions["Move"].performed -= MoveInput_Performed;
        playerInput.actions["Move"].canceled -= MoveInput_Canceled;
        playerInput.actions["Interact"].performed -= InteractInput_Performed;
    }

    private void OnMove()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            if (Time.fixedTime - snapshot > buttonChangeDelay)
            {
                snapshot = Time.fixedTime;
                DetermineDirection();
            }
        } else if (PauseManager.Singleton.IsPaused || BaseController.Singleton.isGameOver)
            DetermineDirection();
    }

    private void DetermineDirection()
    {
        if (MoveInput.magnitude <= 0.1f)
            return;

        if (MoveInput.y > 0)
            ControllerButtonNavigator.currentButton.buttonUp.SetCurrentButton();
        else
            ControllerButtonNavigator.currentButton.buttonDown.SetCurrentButton();
    }

    private void Select()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
            ControllerButtonNavigator.currentButton.PressButton();
        else if (PauseManager.Singleton.IsPaused || BaseController.Singleton.isGameOver)
            ControllerButtonNavigator.currentButton.PressButton();
    }
}
