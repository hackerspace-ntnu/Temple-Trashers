using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    NONE,
    NORTH,
    SOUTH,
    EAST,
    WEST,
}

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonsUI;

    [SerializeField]
    private bool focused;

    private Camera mainCamera;

    void Awake()
    {
        if (buttonsUI == null)
        {
            buttonsUI = new[]
            {
                transform.GetChild(0).gameObject,
                transform.GetChild(0).GetChild(0).gameObject,
                transform.GetChild(0).GetChild(1).gameObject,
                transform.GetChild(0).GetChild(2).gameObject,
                transform.GetChild(0).GetChild(3).gameObject,
            };
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    public void SetButton(Direction button, bool state)
    {
        buttonsUI[(int)button].SetActive(state);

        UpdateUI();
    }

    private void UpdateUI()
    {
        // If no actions are possible, disable the base ui element.
        if (!buttonsUI[(int)Direction.NORTH]
            && !buttonsUI[(int)Direction.SOUTH]
            && !buttonsUI[(int)Direction.EAST]
            && !buttonsUI[(int)Direction.WEST])
        {
            buttonsUI[(int)Direction.NONE].SetActive(false);
        } else if (focused)
            buttonsUI[(int)Direction.NONE].SetActive(true);
    }

    public void Focus()
    {
        focused = true;
        UpdateUI();
    }

    public void Unfocus()
    {
        focused = false;
        buttonsUI[(int)Direction.NONE].SetActive(false);
    }
}
