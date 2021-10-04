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
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.actions["MouseClick"].performed += ctx => OnClick(mousePos);
        input.actions["MouseMove"].performed += ctx => { this.mousePos = ctx.ReadValue<Vector2>(); };
    }

    private void OnDestroy()
    {
        input.actions["MouseClick"].performed -= ctx => OnClick(mousePos);
        input.actions["MouseMove"].performed -= ctx => { this.mousePos = ctx.ReadValue<Vector2>(); };
    }
}
