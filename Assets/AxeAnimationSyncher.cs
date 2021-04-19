using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationSyncher : MonoBehaviour
{
    public AxeTowerController controller;

    public void animationGrabEvent()
    {
        controller.grab();
    }
    public void animationTossEvent()
    {
        controller.toss();
    }
    public void animationSpawnEvent()
    {
        controller.spawn();
    }
}
