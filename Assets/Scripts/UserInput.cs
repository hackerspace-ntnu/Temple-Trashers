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

            UserInput input = player_1.GetComponent<UserInput>();
            input.OnInteractPressed += myFunc;

            // Now myFunc will be run whenever the "interact"-key is pressed by player 1
            // nyfunc needs to be a void function without parameters, because that is the form of the button_delegate() defined below

            // To remove the function from the delegate, use the following:
            
            input.OnInteractPressed -= myFunc;

            // Now myFunc will no longer run when the interact key gets pressed 
            // This is important to prevent an object getting input after the player is done with it
    */

    // Defining a general button-event delegate, this defines the form of every function that can be added to the delegates
    // In this case the functions must be void functions without any parameters
    public delegate void button_delegate();

    [Header("Input axes names")]


    [SerializeField]
    private string move_vertical_name;

    [SerializeField]
    private string move_horizontal_name;

    [SerializeField]
    private string aim_vertical_name;

    [SerializeField]
    private string aim_horizontal_name;


    [Header("Input button names")]
    [SerializeField]
    private string interact_name;

    [SerializeField]
    private string back_name;

    [SerializeField]
    private string start_name;

 

    private Vector2 move_input, aim_input;
    private bool interact_held = false, back_held = false, start_held = false;

    // Setting up the delegates for each button event
    // Each of these delegate-pairs correspond to a specific button on a players controller
    // Designate a funtion to Interact if the functionality is interacting with an object, ie taking controll of a turret
    // --||-- Back if the functionality is exiting a menu or similar
    // --||-- Start is usually for opening a start menu, but may be used differently in a game-over state or similar.
    public button_delegate 
        OnInteractPressed, 
        OnInteractReleased, 
        OnBackPressed, 
        OnBackReleased, 
        OnStartPressed, 
        OnStartReleased;

    void Start()
    {
        updateInput();
    }

    void Update()
    {
        updateInput();
    }

    private void updateInput()
    {

        //Updates the aim and movement directions, and makes sure they're at most unit-length

        move_input = new Vector2(Input.GetAxis(move_horizontal_name), Input.GetAxis(move_vertical_name));
        if(move_input.sqrMagnitude > 1) { move_input.Normalize(); }

        aim_input = new Vector2(Input.GetAxis(aim_horizontal_name), Input.GetAxis(aim_vertical_name)).normalized;
        if(aim_input.sqrMagnitude > 1) { aim_input.Normalize(); }

        // Updates each button to see whether it is being held down, and invokes the Pressed/Released functions if 
        // the button was pressed/released this frame

        interact_held = Input.GetButton(interact_name);
        if (Input.GetButtonDown(interact_name))  { OnInteractPressed?.Invoke(); }
        if (Input.GetButtonUp(interact_name))    { OnInteractReleased?.Invoke(); }

        back_held = Input.GetButton(back_name);
        if (Input.GetButtonDown(back_name))  { OnBackPressed?.Invoke(); }
        if (Input.GetButtonUp(back_name))    { OnBackReleased?.Invoke(); }

        start_held = Input.GetButton(start_name);
        if (Input.GetButtonDown(start_name)) { OnStartPressed?.Invoke(); }  
        if (Input.GetButtonUp(start_name))   { OnStartReleased?.Invoke(); }
    }
    //Returns the current direction the player wants to move
    public Vector2 MoveInput { get => move_input; }
    //Returns the current direction the player wants to aim
    public Vector2 AimInput { get => aim_input; }


    // These functions may be used instead of the delegates if you only care about whether a button is held down or not
    // Returns true if the button is being held down, false if not
    public bool InteractHeld { get => interact_held; }
    public bool BackHeld { get => back_held; }
    public bool StartHeld { get => start_held; }
}
