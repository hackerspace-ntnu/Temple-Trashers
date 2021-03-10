using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private UserInput input;


    public void setUserInput(UserInput currentIpnut)
    {
        input = currentIpnut;
    }
    public UserInput getUserInput()
    {
        return input;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(input.AimInput.y, input.AimInput.x) * 180 - 90 / Mathf.PI;

    }
}
