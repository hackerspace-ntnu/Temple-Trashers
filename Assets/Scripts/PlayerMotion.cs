using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMotion : MonoBehaviour
{
    /*
     *  Controlls the player movement and aligns the player towards the movement direction
     *  
     *  Also currently updates the animation of the player to match the movement speed;
    */

  

    [SerializeField]
    private float playerSpeed, playerAcceleration;

    


    private PlayerControls controls;
    private Vector2 move;


    private PlayerStateController state;
    private UserInput input;
    private Rigidbody body;

    //Temporary solution, have not yet decided upon exact player component hierarchy
    [SerializeField]
    private Animator anim;

    

    //Sets up required instances for input to work. 
    void Awake()
    {
        controls = new PlayerControls();
        controls.MovePlayer.Select.performed += ctx => Sel();
        controls.MovePlayer.Interact.performed += ctx => Inter();
        controls.MovePlayer.Back.performed += ctx => Backk();

        controls.MovePlayer.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.MovePlayer.Move.canceled += ctx => move = Vector2.zero;
    }

    



    void Start()
    {
        
        state = GetComponent<PlayerStateController>();
        input = GetComponent<UserInput>();
        body = GetComponent<Rigidbody>();
    }

    

    private void FixedUpdate()
    {
        updateSpeed();
        updateRotation();
    }

    private void updateSpeed()
    {



        if (state.CurrentState == PlayerStateController.PlayerStates.Dead) { return; }
        // Debug.Log("Move");

        Vector2 m = new Vector2(move.x, move.y) * 2f;

        Vector3 currentspeed = body.velocity;
        Vector3 speedDifference = new Vector3(m.x * playerSpeed - currentspeed.x, 0f, m.y* playerSpeed - currentspeed.z).normalized;

        body.AddForce(speedDifference * playerAcceleration * m.magnitude * Time.fixedDeltaTime, ForceMode.VelocityChange);

        anim.SetFloat("Speed", m.sqrMagnitude);
    }

    private void updateRotation()
    {
        Vector2 m = new Vector2(move.x, move.y);
        if (m.sqrMagnitude > 0.001f)
        {
            body.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(m.x, 0f, m.y), Vector3.up),
                Time.fixedDeltaTime * 360f * 3f
            ) ;
        }
    }

    //Sends a log-message when button is pressed. 
    void Sel()
    {
        Debug.Log("Select is pressed ");
    }
    void Inter()
    {
        Debug.Log("Interact is pressed ");
    }
    void Backk()
    {
        Debug.Log("Back is pressed ");
    }

    private void OnEnable()
    {
        controls.MovePlayer.Enable(); 
      
    }

    private void OnDisable()
    {
        controls.MovePlayer.Disable(); 
    }

}
