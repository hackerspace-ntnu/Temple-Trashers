using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerButtonNavigator : MonoBehaviour
{
    public static ControllerButtonNavigator currentButton;
    public ControllerButtonNavigator buttonUp;
    public ControllerButtonNavigator buttonDown;
    public static ControllerButtonNavigator defaultButton;


    private Color normalColor;
    private Color highlightColor;
    private Color pressedColor;
    

    void Start()
    {
        normalColor = GetComponent<Button>().colors.normalColor;
        highlightColor = GetComponent<Button>().colors.highlightedColor;
        pressedColor = GetComponent<Button>().colors.pressedColor;

        GetComponent<Image>().color = normalColor;

        if (!currentButton)
        {
            if (!defaultButton) { defaultButton = this; }
            currentButton = defaultButton;
            currentButton.GetComponent<Image>().color = highlightColor;
        }
    }

    public void SetCurrentButton()
    {
        //Reset highlight of previous currentButton
        currentButton.GetComponent<Image>().color = currentButton.normalColor;
        currentButton = this;
        currentButton.GetComponent<Image>().color = highlightColor;
    }

    public void PressButton()
    {
        GetComponent<Button>().onClick.Invoke();
        currentButton.GetComponent<Image>().color = pressedColor;
        if (currentButton.isActiveAndEnabled)
        {
            StartCoroutine(ColorReset());
        }
    }

    private void OnDestroy()
    {
        if (currentButton)
        {
            currentButton = null;
            defaultButton = null;
        }
    }

    IEnumerator ColorReset()
    {
        yield return new WaitForSeconds(0.05f);
        currentButton.GetComponent<Image>().color = highlightColor;
        
    }
}
