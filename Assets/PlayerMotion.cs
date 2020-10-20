using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed, playerAcceleration;

    private UserInput input;
    private Rigidbody body;
    void Start()
    {
        input = GetComponent<UserInput>();
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 currentspeed = body.velocity;
        Vector3 acceleratedSpeed = currentspeed + new Vector3(input.MoveInput.x, 0f, input.MoveInput.y) * playerAcceleration * Time.fixedDeltaTime;
        if(acceleratedSpeed.sqrMagnitude > playerSpeed * playerSpeed)
        {
            acceleratedSpeed = acceleratedSpeed.normalized * playerSpeed;
        }
        print(acceleratedSpeed-currentspeed);
        body.AddForce(acceleratedSpeed-currentspeed, ForceMode.VelocityChange);
    }

}
