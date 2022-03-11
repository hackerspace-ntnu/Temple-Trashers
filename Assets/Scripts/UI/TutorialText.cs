using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonsUI;

    [SerializeField]
    private bool focused;

    private void Awake()
    {
        if(buttonsUI == null)
        {
            buttonsUI = new GameObject[5]
            {
                transform.GetChild(0).gameObject,
                transform.GetChild(0).GetChild(0).gameObject,
                transform.GetChild(0).GetChild(1).gameObject,
                transform.GetChild(0).GetChild(2).gameObject,
                transform.GetChild(0).GetChild(3).gameObject
            };
        }
        
    }

    public enum Direction {
    Base,
    North,
    South,
    East,
    West};

    public void SetButton(Direction button, bool state)
    {
        buttonsUI[(int)button].SetActive(state);

        UpdateUI();
    }

    private void UpdateUI()
    {
        // If no actions are possible, disable the base ui element.
        if (buttonsUI[(int)Direction.North] == false &&
            buttonsUI[(int)Direction.South] == false &&
            buttonsUI[(int)Direction.East] == false &&
            buttonsUI[(int)Direction.West] == false)

            buttonsUI[(int)Direction.Base].SetActive(false);
        else if(focused)
            buttonsUI[(int)Direction.Base].SetActive(true);
    }

    public void Focus()
    {
        focused = true;
        UpdateUI();
    }

    public void Unfocus()
    {
        focused = false;
        buttonsUI[(int)Direction.Base].SetActive(false);
    }
}
