using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationSyncher : MonoBehaviour
{
    public AxeTowerAnimationController animationController;

    public void animationGrabEvent()
    {
        animationController.Grab();
    }

    public void animationTossEvent()
    {
        animationController.Shoot();
    }

    public void animationSpawnEvent()
    {
        animationController.Spawn();
    }
}
