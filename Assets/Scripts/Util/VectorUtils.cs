using UnityEngine;

public static class VectorUtils
{
    public static Vector3 SetOnly(this Vector3 vec, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z);
    }

    public static bool DistanceGreaterThan(this Vector3 vec, float dist, Vector3 other)
    {
        return (other - vec).sqrMagnitude > dist * dist;
    }

    public static bool DistanceLessThan(this Vector3 vec, float dist, Vector3 other)
    {
        return (other - vec).sqrMagnitude < dist * dist;
    }
}
