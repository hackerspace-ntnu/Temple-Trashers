using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerButtonNavigator : MonoBehaviour
{

    public static ControllerButtonNavigator currentButton;
    public ControllerButtonNavigator buttonUp;
    public ControllerButtonNavigator buttonDown;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!currentButton)
        {
            currentButton = this;
        }
    }

    public void setCurrentButton()
    {
        //Reset highlight of previous currentButton
        //currentButton.GetComponent<>();
        currentButton = this;
        //currentButton.GetComponent<>();
    }

    public void pressButton()
    {
        GetComponent<Button>().onClick.Invoke();
    }

}
