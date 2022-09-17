using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MainMenuInputController : UIInputController
{
    private AudioSource uiInputAudio;

    void Awake()
    {
        SetUpInput(GetComponent<PlayerInput>());
        GameObject inputManager = GameObject.Find("UIInputManager");
        if (inputManager)
        {
            uiInputAudio = inputManager.GetComponent<AudioSource>();
        }
        
    }

    protected override void Move()
    {
        ChangeSelectedButton();
        if (uiInputAudio) { uiInputAudio.Play(); }
    }

    protected override void Select()
    {
        ControllerButtonNavigator.currentButton.PressButton();
    }
}
