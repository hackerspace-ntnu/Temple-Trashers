public enum HexDirection
{
    NE,
    E,
    SE,
    SW,
    W,
    NW,
}

public static class HexDirectionExtensions
{
    public static HexDirection Opposite(this HexDirection direction)
    {
        int oppositeDirectionIndex = ((int)direction + 3) % 6;
        return (HexDirection)oppositeDirectionIndex;
    }

    public static HexDirection Previous(this HexDirection direction)
    {
        return direction == HexDirection.NE ? HexDirection.NW : direction - 1;
    }

    public static HexDirection Next(this HexDirection direction)
    {
        return direction == HexDirection.NW ? HexDirection.NE : direction + 1;
    }
}
