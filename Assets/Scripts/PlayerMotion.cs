using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    /*
     *  Controlls the player movement and aligns the player towards the movement direction
     *  
     *  Also currently updates the animation of the player to match the movement speed;
    */
    [SerializeField]
    private float playerSpeed, playerAcceleration;

    private UserInput input;
    private Rigidbody body;

    //Temporary solution, have not yet decided upon exact player component hierarchy
    [SerializeField]
    private Animator anim;

    void Start()
    {
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
        Vector3 currentspeed = body.velocity;
        Vector3 speedDifference = new Vector3(input.MoveInputRaw.x * playerSpeed - currentspeed.x, 0f, input.MoveInputRaw.y * playerSpeed - currentspeed.z).normalized;

        body.AddForce(speedDifference * playerAcceleration * input.MoveInput.magnitude * Time.fixedDeltaTime, ForceMode.VelocityChange);

        anim.SetFloat("Speed", input.MoveInput.sqrMagnitude);
    }

    private void updateRotation()
    {
        if (input.MoveInputRaw.sqrMagnitude > 0.001f)
        {
            body.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(input.MoveInputRaw.x, 0f, input.MoveInputRaw.y), Vector3.up),
                Time.fixedDeltaTime * 360f
            );
        }
    }

}
