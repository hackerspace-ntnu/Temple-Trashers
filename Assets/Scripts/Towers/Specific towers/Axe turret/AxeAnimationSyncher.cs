using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationSyncher : MonoBehaviour
{
    public AxeTowerController controller;

    public void animationGrabEvent()
    {
        controller.Grab();
    }

    public void animationTossEvent()
    {
        controller.Shoot();
    }

    public void animationSpawnEvent()
    {
        controller.Spawn();
    }
}
