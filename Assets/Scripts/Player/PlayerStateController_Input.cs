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

    #region D-pad

    public bool DLeft { get; private set; } = false;
    public bool DRight { get; private set; } = false;
    public bool DUp { get; private set; } = false;
    public bool DDown { get; private set; } = false;

    #endregion D-pad

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

    #region D-pad

    private void DLeft_Performed(InputAction.CallbackContext ctx)
    {
        DLeft = true;
        OnDLeft();
    }

    private void DLeft_Canceled(InputAction.CallbackContext ctx) => DLeft = false;

    private void DRight_Performed(InputAction.CallbackContext ctx)
    {
        DRight = true;
        OnDRight();
    }

    private void DRight_Canceled(InputAction.CallbackContext ctx) => DRight = false;

    private void DUp_Performed(InputAction.CallbackContext ctx)
    {
        DUp = true;
        OnDUp();
    }

    private void DUp_Canceled(InputAction.CallbackContext ctx) => DUp = false;

    private void DDown_Performed(InputAction.CallbackContext ctx)
    {
        DDown = true;
        OnDDown();
    }

    private void DDown_Canceled(InputAction.CallbackContext ctx) => DDown = false;

    #endregion D-pad

    private void MoveTowerInput_Performed(InputAction.CallbackContext ctx) => OnMoveTower();

    private void ReadyForNextWaveInput_Performed(InputAction.CallbackContext ctx) => EnemyWaveManager.ReadyForNextWave();

    // Called by `PlayerSpecificManager` after instantiating the player
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
        newInput.actions["Move tower"].performed += MoveTowerInput_Performed;

        #region D-pad

        newInput.actions["DLeft"].performed += DLeft_Performed;
        newInput.actions["DLeft"].canceled += DLeft_Canceled;
        newInput.actions["DRight"].performed += DRight_Performed;
        newInput.actions["DRight"].canceled += DRight_Canceled;
        newInput.actions["DUp"].performed += DUp_Performed;
        newInput.actions["DUp"].canceled += DUp_Canceled;
        newInput.actions["DDown"].performed += DDown_Performed;
        newInput.actions["DDown"].canceled += DDown_Canceled;

        #endregion D-pad

        #region Developer hotkeys

        newInput.actions["Ready for next wave"].performed += ReadyForNextWaveInput_Performed;

        #endregion Developer hotkeys
    }

    private void OnDestroy_Input()
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
            input.actions["Move tower"].performed -= MoveTowerInput_Performed;

            #region D-pad

            input.actions["DLeft"].performed -= DLeft_Performed;
            input.actions["DLeft"].canceled -= DLeft_Canceled;
            input.actions["DRight"].performed -= DRight_Performed;
            input.actions["DRight"].canceled -= DRight_Canceled;
            input.actions["DUp"].performed -= DUp_Performed;
            input.actions["DUp"].canceled -= DUp_Canceled;
            input.actions["DDown"].performed -= DDown_Performed;
            input.actions["DDown"].canceled -= DDown_Canceled;

            #endregion D-pad

            #region Developer hotkeys

            input.actions["Ready for next wave"].performed -= ReadyForNextWaveInput_Performed;

            #endregion Developer hotkeys
        }
    }

    #region D-pad

    private void OnDLeft()
    {
        DoDPadAction(0);
    }

    private void OnDRight()
    {
        DoDPadAction(1);
    }

    private void OnDUp()
    {
        DoDPadAction(0);
    }

    private void OnDDown()
    {
        DoDPadAction(1);
    }

    private void DoDPadAction(int towerIndex)
    {
        TowerScriptableObject tower = ui.ControllerWheel.GetTower(towerIndex);
        if (inventoryManager.ResourceAmount < tower.Cost)
            messageUI.DisplayMessage("Not enough crystals", MessageUI.TextColors.red);
        if (_currentState == PlayerStates.FREE && inventoryManager.ResourceAmount >= tower.Cost)
        {
            inventoryManager.ResourceAmount -= tower.Cost;
            tower.InstantiateConstructionTower(this);
            SetState(PlayerStates.BUILDING);
        }
    }

    #endregion D-pad
}
