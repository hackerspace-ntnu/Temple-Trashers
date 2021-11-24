using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputController : MonoBehaviour
{
    public delegate void MouseClickEvent(Vector2 pos);

    public static MouseClickEvent OnClick;
    private Vector2 mousePos;
    private PlayerInput input;

    private void MouseClick_Performed(InputAction.CallbackContext ctx) => OnClick(mousePos);
    private void MouseMove_Performed(InputAction.CallbackContext ctx) => mousePos = ctx.ReadValue<Vector2>();

    void Awake()
    {
        input = GetComponent<PlayerInput>();

        input.actions["MouseClick"].performed += MouseClick_Performed;
        input.actions["MouseMove"].performed += MouseMove_Performed;
    }

    void OnDestroy()
    {
        input.actions["MouseClick"].performed -= MouseClick_Performed;
        input.actions["MouseMove"].performed -= MouseMove_Performed;
    }
}
