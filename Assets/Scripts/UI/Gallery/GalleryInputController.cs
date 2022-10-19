using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GalleryInputController : UIInputController
{
    private void CancelInput_Performed(InputAction.CallbackContext ctx) => SceneManager.LoadScene("Main_Menu");

    private void CancelInput_Canceled(InputAction.CallbackContext ctx)
    { }

    void OnDestroy()
    {
        if (playerInput)
            RemoveListeners();
    }

    void Awake()
    {
        SetUpInput(GetComponent<PlayerInput>());
    }

    protected override void Move()
    {
        ChangeSelectedButton();
    }

    protected override void Select()
    {
        ControllerButtonNavigator.currentButton.PressButton();
    }

    protected override void AddListeners()
    {
        playerInput.actions["Move"].performed += MoveInput_Performed;
        playerInput.actions["Move"].canceled += MoveInput_Canceled;
        playerInput.actions["Interact"].performed += InteractInput_Performed;
        playerInput.actions["Cancel"].performed += CancelInput_Performed;
        playerInput.actions["Cancel"].canceled += CancelInput_Canceled;
    }

    protected override void RemoveListeners()
    {
        playerInput.actions["Move"].performed -= MoveInput_Performed;
        playerInput.actions["Move"].canceled -= MoveInput_Canceled;
        playerInput.actions["Interact"].performed -= InteractInput_Performed;
        playerInput.actions["Cancel"].performed -= CancelInput_Performed;
        playerInput.actions["Cancel"].canceled -= CancelInput_Canceled;
    }
}
