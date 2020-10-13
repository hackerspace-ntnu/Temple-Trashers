using UnityEngine;

public static class HexMetrics
{
    public const int chunkSizeX = 5, chunkSizeZ = 5;

    public const float outerRadius = 1f;

    public const float innerRadius = outerRadius * 0.866025404f;

    public const float solidfactor = 0.8f;

    public const float blendFactor = 1f - solidfactor;

    static Vector3[] corners =
    {
        new Vector3(0f,0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };

    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner (HexDirection direction)
    {
        return corners[(int)direction] * solidfactor;
    }

    public static Vector3 GetSecondSolidCorner (HexDirection direction)
    {
        return corners[(int)direction + 1] * solidfactor;
    }

    public static Vector3 GetBridge (HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) * 0.5f * blendFactor;
    }
}
