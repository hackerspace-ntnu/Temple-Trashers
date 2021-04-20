using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMotion : MonoBehaviour
{
    /*
     *  Controlls the player movement and aligns the player towards the movement direction
     *  
     *  Also currently updates the animation of the player to match the movement speed;
    */

    [SerializeField]
    private float playerSpeed = 0f, playerAcceleration = 0f;
    private PlayerStateController state = null;
    private Rigidbody body = null;

    //Temporary solution, have not yet decided upon exact player component hierarchy
    [SerializeField]
    private Animator anim = null;

    //Sets up required instances for input to work. 
    void Start()
    {

        state = GetComponent<PlayerStateController>();
        body = GetComponent<Rigidbody>();
    }

    public void move()
    {
        updateSpeed();
        updateRotation();
    }

    private void updateSpeed()
    {
        if (state.CurrentState == PlayerStateController.PlayerStates.Dead) { return; }

        Vector2 m = new Vector2(state.MoveInput.x, state.MoveInput.y) * 2f;

        Vector3 currentspeed = body.velocity;
        Vector3 speedDifference = new Vector3(m.x * playerSpeed - currentspeed.x, 0f, m.y* playerSpeed - currentspeed.z).normalized;

        body.AddForce(speedDifference * playerAcceleration * m.magnitude * Time.fixedDeltaTime, ForceMode.VelocityChange);

        anim.SetFloat("Speed", m.sqrMagnitude);
    }

    private void updateRotation()
    {
        Vector2 m = new Vector2(state.MoveInput.x, state.MoveInput.y);
        if (m.sqrMagnitude > 0.001f)
        {
            body.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(m.x, 0f, m.y), Vector3.up),
                Time.fixedDeltaTime * 360f * 3f
            );
        }
    }
}
