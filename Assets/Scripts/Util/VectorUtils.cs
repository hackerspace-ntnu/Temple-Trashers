using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils
{
    public static Vector3 SetOnly(this Vector3 vec, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z);
    }
}
