using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerButtonNavigator : MonoBehaviour
{
    public static ControllerButtonNavigator currentButton;
    public ControllerButtonNavigator buttonUp;
    public ControllerButtonNavigator buttonDown;
    public ControllerButtonNavigator defaultButton;

    [SerializeField]
    private Color normalColor;

    [SerializeField]
    private Color highlightColor;

    void Start()
    {
        if (!currentButton)
        {
            currentButton = defaultButton;
            currentButton.GetComponent<Image>().color = highlightColor;
        }
    }

    public void SetCurrentButton()
    {
        //Reset highlight of previous currentButton
        //There are better ways to do this, but Unity refused to cooperate :(
        currentButton.GetComponent<Image>().color = normalColor;
        currentButton = this;
        currentButton.GetComponent<Image>().color = highlightColor;
    }

    public void PressButton()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
