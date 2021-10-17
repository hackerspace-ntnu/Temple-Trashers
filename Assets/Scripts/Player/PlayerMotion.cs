using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player movement and aligns the player towards the movement direction.
/// Also currently updates the animation of the player to match the movement speed.
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 0f;

    [SerializeField]
    private float playerAcceleration = 0f;

    private PlayerStateController state;
    private Rigidbody body;

    //Temporary solution, have not yet decided upon exact player component hierarchy
    [SerializeField]
    private Animator anim = null;

    //Sets up required instances for input to work. 
    void Start()
    {
        state = GetComponent<PlayerStateController>();
        body = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        UpdateSpeed();
        UpdateRotation();
    }

    private void UpdateSpeed()
    {
        if (state.CurrentState == PlayerStates.DEAD)
            return;

        Vector2 scaledMoveInput = new Vector2(state.MoveInput.x, state.MoveInput.y) * 2f;

        Vector3 currentSpeed = body.velocity;
        Vector3 speedDifference = new Vector3(
            scaledMoveInput.x * playerSpeed - currentSpeed.x,
            0f,
            scaledMoveInput.y * playerSpeed - currentSpeed.z
        ).normalized;

        Vector3 force = speedDifference * playerAcceleration * scaledMoveInput.magnitude * Time.fixedDeltaTime;
        body.AddForce(force, ForceMode.VelocityChange);

        anim.SetFloat("Speed", body.velocity.sqrMagnitude/Mathf.Pow(playerSpeed,2));
    }

    private void UpdateRotation()
    {
        Vector2 moveInput = new Vector2(state.MoveInput.x, state.MoveInput.y);
        if (moveInput.sqrMagnitude <= 0.001f)
            return;

        body.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(new Vector3(moveInput.x, 0f, moveInput.y), Vector3.up),
            Time.fixedDeltaTime * 360f * 3f
        );
    }
}
