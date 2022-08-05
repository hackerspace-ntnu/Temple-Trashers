using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MainMenuInputController : UIInputController
{
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
}
