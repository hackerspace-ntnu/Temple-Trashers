public static class MathUtils
{
    /// <returns>
    ///     The same as <c>Mathf.Sign()</c>, but the return type is <c>int</c>,
    ///     and returns <c>0</c> if <c>f</c> is exactly equal to <c>0</c>.
    /// </returns>
    public static int StrictSign(float f)
    {
        // It's uncommon for `f` to equal 0, so take a more common case first:
        if (f > 0f)
            return 1;
        return f == 0f ? 0 : -1;
    }

    /// <returns>The provided angle, normalized between 0 and 360 degrees.</returns>
    public static float NormalizeDegreeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;

        return angle;
    }
}
