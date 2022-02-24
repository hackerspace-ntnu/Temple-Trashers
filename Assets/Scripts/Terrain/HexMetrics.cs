using UnityEngine;


public static class HexMetrics
{
    public const float OUTER_RADIUS = 1f;

    public static readonly float INNER_RADIUS = OUTER_RADIUS * Mathf.Sqrt(3) / 2f;

    public const float ELEVATION_STEP = 0.1f;

    public static float noiseScale = 0.003f;

    public const int CHUNK_SIZE_X = 5;
    public const int CHUNK_SIZE_Z = 5;

    public static Vector4 SampleNoise(Vector3 position, Texture2D noiseSource)
    {
        return noiseSource.GetPixelBilinear(
            position.x * noiseScale,
            position.z * noiseScale
        );
    }
}
