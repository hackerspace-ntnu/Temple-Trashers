using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ray
{
    public Transform ray;
    public Transform target;

    public Ray(Transform ray, Transform target)
    {
        this.ray = ray;
        this.target = target;
    }
}
