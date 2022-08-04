using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MainMenuInputController : UIInputController
{
    [SerializeField]
    private float buttonChangeDelay = 0.2f;

    private float snapshot;

    void Awake()
    {
        SetUpInput(GetComponent<PlayerInput>());
    }

    protected override void OnMove()
    {
        if (Time.fixedTime - snapshot > buttonChangeDelay)
        {
            snapshot = Time.fixedTime;
            DetermineDirection();
        }
    }

    protected override void Select()
    {
        ControllerButtonNavigator.currentButton.PressButton();
    }
}
