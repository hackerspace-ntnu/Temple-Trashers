using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

partial class PlayerStateController
{
    private PlayerInput input; // Controller input

    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 AimInput { get; private set; } = Vector2.zero;
    public bool Interact { get; private set; } = false;
    public bool Cancel { get; private set; } = false;
    public bool Select { get; private set; } = false;

    private void MoveInput_Performed(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void MoveInput_Canceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    private void AimInput_Performed(InputAction.CallbackContext ctx) => AimInput = ctx.ReadValue<Vector2>();
    private void AimInput_Canceled(InputAction.CallbackContext ctx) => AimInput = Vector2.zero;
    private void InteractInput_Performed(InputAction.CallbackContext ctx) => OnInteract();
    private void CancelInput_Performed(InputAction.CallbackContext ctx) => Cancel = true;
    private void CancelInput_Canceled(InputAction.CallbackContext ctx) => Cancel = false;
    private void SelectInput_Performed(InputAction.CallbackContext ctx) => Select = true;
    private void SelectInput_Canceled(InputAction.CallbackContext ctx) => Select = false;

    private void PauseInput_Performed(InputAction.CallbackContext ctx) => PauseManager.Singleton.PauseGame();

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
        newInput.actions["Pause"].performed += PauseInput_Performed;

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
            input.actions["Pause"].performed -= PauseInput_Performed;

            input.actions["Ready for next wave"].performed -= ReadyForNextWaveInput_Canceled;
        }
    }
}
