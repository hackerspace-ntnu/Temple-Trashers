using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    //Public variables
    [Header("UI Elements")]
    [SerializeField]
    private GameObject northUI;
    [SerializeField]
    private GameObject southUI;
    [SerializeField]
    private GameObject eastUI;
    [SerializeField]
    private GameObject westUI;
    [SerializeField]
    private GameObject baseUI;

    private bool focused = false;

    public void SetEnabledElements(bool north, bool south, bool east, bool west)
    {
        northUI.SetActive(north);
        southUI.SetActive(south);
        eastUI.SetActive(east);
        westUI.SetActive(west);

        // If we are focused, ensure that the base ui element is enabled
        if (focused)
            baseUI.SetActive(true);

        // If no actions are possible, disable the base ui element.
        if (north == false && south == false && east == false && west == false)
            baseUI.SetActive(false);

        
    }

    public void Focus()
    {
        focused = true;
        if(northUI.activeSelf == true || southUI.activeSelf == true || eastUI.activeSelf == true || westUI.activeSelf == true)
            baseUI.SetActive(true);
    }

    public void Unfocus()
    {
        focused = false;
        baseUI.SetActive(false);
    }
}
