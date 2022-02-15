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
        _fxLow = Instantiate(fxLow, gameObject.transform);
        _fxMedium = Instantiate(fxMedium, gameObject.transform);
        _fxHigh = Instantiate(fxHigh, gameObject.transform);
        DisableInstances();
        onWearStateChange += UpdateParticles;
    }

    public void DisableInstances()
    {
        if (_fxLow) { _fxLow.SetActive(false); }
        if (_fxMedium) { _fxMedium.SetActive(false); }
        if (_fxHigh) { _fxHigh.SetActive(false); }
    }

    public void UpdateParticles(WearState newState, WearState previousState)
    {
       switch (newState)
        {
            case WearState.NONE:
                DisableInstances();
                break;
            case WearState.LOW:
                _fxLow.SetActive(true);
                break;
            case WearState.MEDIUM:
                _fxMedium.SetActive(true);
                break;
            case WearState.HIGH:
                _fxHigh.SetActive(true);
                break;
            default:
                DisableInstances();
                break;
        }
    }
}
