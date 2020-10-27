using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    /*
        How to use: 
        If you want a function to execute once for each push or release of a specific button, simply add that function
        to the corresponding delegate among the ones defined below.
        Example:

            UserInput input = player1.GetComponent<UserInput>();
            input.OnInteractPressed += myFunc;

            // Now myFunc will be run whenever the "interact" key is pressed by player 1
            // myFunc needs to be a void function without parameters, because that is the form of the ButtonDelegate() defined below

            // To remove the function from the delegate, use the following:
            
            input.OnInteractPressed -= myFunc;

            // Now myFunc will no longer run when the interact key gets pressed 
            // This is important to prevent an object getting input after the player is done with it
    */

    // Defining a general button event delegate, this defines the form of every function that can be added to the delegates
    // In this case the functions must be void functions without any parameters
    public delegate void ButtonDelegate();

    [Header("Input axes names")]
    [SerializeField]
    private string moveVerticalName;

    [SerializeField]
    private string moveHorizontalName;

    [SerializeField]
    private string aimVerticalName;

    [SerializeField]
    private string aimHorizontalName;


    [Header("Input button names")]
    [SerializeField]
    private string interactName;

    [SerializeField]
    private string backName;

    [SerializeField]
    private string startName;


    private Vector2 moveInput, aimInput, moveInputRaw, aimInputRaw;
    private bool interactHeld = false, backHeld = false, startHeld = false;

    // Setting up the delegates for each button event
    // Each of these delegate pairs correspond to a specific button on a players controller
    // Designate a function to interact if the functionality is interacting with an object, i.e. taking control of a turret
    // --||-- Back if the functionality is exiting a menu or similar
    // --||-- Start is usually for opening a start menu, but may be used differently in a game over state or similar.
    public ButtonDelegate
        OnInteractPressed,
        OnInteractReleased,
        OnBackPressed,
        OnBackReleased,
        OnStartPressed,
        OnStartReleased;

  
    void speedBoost() { }
    void Start()
    {
        UpdateInput();
    }

    void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        // Updates the aim and movement directions, and makes sure they're at most unit-length

        moveInput = new Vector2(Input.GetAxis(moveHorizontalName), Input.GetAxis(moveVerticalName));
        if (moveInput.sqrMagnitude > 1)
            moveInput.Normalize();
        moveInputRaw = new Vector2(Input.GetAxisRaw(moveHorizontalName), Input.GetAxisRaw(moveVerticalName));
        if (moveInputRaw.sqrMagnitude > 1)
            moveInputRaw.Normalize();

        aimInput = new Vector2(Input.GetAxis(aimHorizontalName), Input.GetAxis(aimVerticalName));
        if (aimInput.sqrMagnitude > 1)
            aimInput.Normalize();
        aimInputRaw = new Vector2(Input.GetAxisRaw(aimHorizontalName), Input.GetAxisRaw(aimVerticalName));
        if (aimInputRaw.sqrMagnitude > 1)
            aimInputRaw.Normalize();

        // Updates each button to see whether it is being held down, and invokes the Pressed/Released functions if 
        // the button was pressed/released this frame

        interactHeld = Input.GetButton(interactName);
        if (Input.GetButtonDown(interactName))
            OnInteractPressed?.Invoke();
        if (Input.GetButtonUp(interactName))
            OnInteractReleased?.Invoke();

        backHeld = Input.GetButton(backName);
        if (Input.GetButtonDown(backName))
            OnBackPressed?.Invoke();
        if (Input.GetButtonUp(backName))
            OnBackReleased?.Invoke();

        startHeld = Input.GetButton(startName);
        if (Input.GetButtonDown(startName))
            OnStartPressed?.Invoke();
        if (Input.GetButtonUp(startName))
            OnStartReleased?.Invoke();
    }

    //Returns the current direction the player wants to move
    public Vector2 MoveInput => moveInput;
    public Vector2 MoveInputRaw => moveInputRaw;

    //Returns the current direction the player wants to aim
    public Vector2 AimInput => aimInput;
    public Vector2 AimInputRaw => aimInputRaw;


    // These functions may be used instead of the delegates if you only care about whether a button is held down or not
    // Returns true if the button is being held down, false if not
    public bool InteractHeld => interactHeld;
    public bool BackHeld => backHeld;
    public bool StartHeld => startHeld;

}
