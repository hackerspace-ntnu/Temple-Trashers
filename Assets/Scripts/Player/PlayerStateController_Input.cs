using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

partial class PlayerStateController
{
    private PlayerInput input; // Controller input

    private Vector2 moveInput = Vector2.zero;
    private Vector2 aimInput = Vector2.zero;
    private bool interact = false;
    private bool cancel = false;
    private bool select = false;

    public Vector2 MoveInput { get => moveInput; }
    public Vector2 AimInput { get => aimInput; }
    public bool Interact { get => interact; }
    public bool Cancel { get => cancel; }
    public bool Select { get => select; }

    private void MoveInput_Performed(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
    private void MoveInput_Canceled(InputAction.CallbackContext ctx) => moveInput = Vector2.zero;
    private void AimInput_Performed(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    private void AimInput_Canceled(InputAction.CallbackContext ctx) => aimInput = Vector2.zero;
    private void InteractInput_Performed(InputAction.CallbackContext ctx) => OnInteract();
    private void CancelInput_Performed(InputAction.CallbackContext ctx) => cancel = true;
    private void CancelInput_Canceled(InputAction.CallbackContext ctx) => cancel = false;
    private void SelectInput_Performed(InputAction.CallbackContext ctx) => select = true;
    private void SelectInput_Canceled(InputAction.CallbackContext ctx) => select = false;

    private void ReadyForNextWaveInput_Canceled(InputAction.CallbackContext ctx) => EnemyWaveManager.ReadyForNextWave();

    public void SetUpInput(PlayerInput newInput, PlayerSpecificManager newManager)
    {
        input = newInput;
        manager = newManager;

        newInput.actions["Move"].performed += MoveInput_Performed;
        newInput.actions["Move"].canceled += MoveInput_Canceled;
        newInput.actions["Aim"].performed += AimInput_Performed;
        newInput.actions["Aim"].canceled += AimInput_Canceled;
        newInput.actions["Interact"].performed += InteractInput_Performed;
        newInput.actions["Cancel"].performed += CancelInput_Performed;
        newInput.actions["Cancel"].canceled += CancelInput_Canceled;
        newInput.actions["Select"].performed += SelectInput_Performed;
        newInput.actions["Select"].canceled += SelectInput_Canceled;

        #region Developer hotkeys

        newInput.actions["Ready for next wave"].performed += ReadyForNextWaveInput_Canceled;

        #endregion Developer hotkeys
    }

    void OnDestroy()
    {
        if (input)
        {
            input.actions["Move"].performed -= MoveInput_Performed;
            input.actions["Move"].canceled -= MoveInput_Canceled;
            input.actions["Aim"].performed -= AimInput_Performed;
            input.actions["Aim"].canceled -= AimInput_Canceled;
            input.actions["Interact"].performed -= InteractInput_Performed;
            input.actions["Cancel"].performed -= CancelInput_Performed;
            input.actions["Cancel"].canceled -= CancelInput_Canceled;
            input.actions["Select"].performed -= SelectInput_Performed;
            input.actions["Select"].canceled -= SelectInput_Canceled;

            input.actions["Ready for next wave"].performed -= ReadyForNextWaveInput_Canceled;
        }
    }
}
