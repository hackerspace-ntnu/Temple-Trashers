using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    //private play
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //playerInput.actions["Move"] += ;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerButtonNavigator.currentButton.buttonUp.setCurrentButton();
    }
}
