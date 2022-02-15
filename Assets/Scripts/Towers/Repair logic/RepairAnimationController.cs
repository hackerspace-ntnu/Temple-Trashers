using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAnimationController : RepairController
{
    //Class references
    public GameObject fxLow;
    public GameObject fxMedium;
    public GameObject fxHigh;

    //Instances
    private GameObject _fxLow;
    private GameObject _fxMedium;
    private GameObject _fxHigh;

    private void Awake()
    {
        onWearStateChange += UpdateParticles;
    }

    public void DeleteInstances()
    {
        if (_fxLow) { Destroy(_fxLow); }
        if (_fxMedium) { Destroy(_fxMedium); }
        if (_fxHigh) { Destroy(_fxHigh); }
    }

    public void UpdateParticles(WearState newState, WearState previousState)
    {
       switch (newState)
        {
            case WearState.NONE:
                DeleteInstances();
                break;
            case WearState.LOW:
                _fxLow = Instantiate(fxLow, gameObject.transform);
                break;
            case WearState.MEDIUM:
                _fxMedium = Instantiate(fxMedium, gameObject.transform);
                break;
            case WearState.HIGH:
                _fxHigh = Instantiate(fxHigh, gameObject.transform);
                break;
            default:
                DeleteInstances();
                break;
        }
    }
}
