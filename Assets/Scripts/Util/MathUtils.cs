public static class MathUtils
{
    /// <returns>The provided angle, normalized between 0 and 360 degrees.</returns>
    public static float NormalizeDegreeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;

        return angle;
    }
}
