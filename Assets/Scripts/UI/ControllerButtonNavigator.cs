using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerButtonNavigator : MonoBehaviour
{

    public static ControllerButtonNavigator currentButton;
    public ControllerButtonNavigator buttonUp;
    public ControllerButtonNavigator buttonDown;
    public ControllerButtonNavigator defaultButton;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!currentButton)
        {
            currentButton = defaultButton;
            currentButton.GetComponent<Button>().Select();
        }

    }

    public void SetCurrentButton()
    {
        //Reset highlight of previous currentButton
        //currentButton.GetComponent<Button>().
        EventSystem.current.SetSelectedGameObject(null);
        currentButton = this;
        currentButton.GetComponent<Button>().Select();
        //currentButton.GetComponent<>();
    }

    public void PressButton()
    {
        GetComponent<Button>().onClick.Invoke();
    }

}
