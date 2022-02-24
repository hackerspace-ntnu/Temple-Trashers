using System;
using UnityEngine;


[Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int _x;

    [SerializeField]
    private int _z;

    public int X => _x;

    public int Z => _z;

    public int Y => -X - Z;

    public HexCoordinates(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrics.INNER_RADIUS * 2f);
        float y = -x;

        float offset = position.z / (HexMetrics.OUTER_RADIUS * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
                iX = -iY - iZ;
            else if (dZ > dY)
                iZ = -iX - iY;
        }

        return new HexCoordinates(iX, iZ);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public string ToStringOnSeparateLines()
    {
        return $"{X}\n{Y}\n{Z}";
    }
}
