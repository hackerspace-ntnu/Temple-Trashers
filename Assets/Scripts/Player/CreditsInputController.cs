using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CreditsInputController : MonoBehaviour
{
    private PlayerInput input;

    private void CancelMenuInput_Performed(InputAction.CallbackContext ctx) => SceneManager.LoadScene("Main_Menu");

    private void CancelMenuInput_Canceled(InputAction.CallbackContext ctx)
    {}

    void Awake()
    {
        input = GetComponent<PlayerInput>();

        input.actions["Cancel_Menu"].performed += CancelMenuInput_Performed;
        input.actions["Cancel_Menu"].canceled += CancelMenuInput_Canceled;
    }

    void OnDestroy()
    {
        input.actions["Cancel_Menu"].performed -= CancelMenuInput_Performed;
        input.actions["Cancel_Menu"].canceled -= CancelMenuInput_Canceled;
    }
}
