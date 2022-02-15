using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAnimationController : MonoBehaviour
{
    //Class references
    public GameObject fxLow;
    public GameObject fxMedium;
    public GameObject fxHigh;

    //Instances
    private GameObject _fxLow;
    private GameObject _fxMedium;
    private GameObject _fxHigh;

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
                _fxLow = Instantiate(fxLow);
                break;
            case WearState.MEDIUM:
                _fxMedium = Instantiate(fxMedium);
                break;
            case WearState.HIGH:
                _fxHigh = Instantiate(fxHigh);
                break;
            default:
                DeleteInstances();
                break;
        }
    }
}
