using UnityEngine;


public static class HexMetrics {

    public const float outerRadius = 1f;

    public const float innerRadius = outerRadius * 0.866025404f;

    public const float elevationStep = 0.1f;

    public static Texture2D noiseSource;

    public static float noiseScale = 0.003f;

    public const int chunkSizeX = 5, chunkSizeZ = 5;

    public static Vector4 SampleNoise(Vector3 position)
    {
        if(noiseSource == null)
        {
            noiseSource = GameObject.Find("Terrain").GetComponent<HexGrid>().noise;
        }
        return noiseSource.GetPixelBilinear(
            position.x * noiseScale, 
            position.z * noiseScale
            );
    }
}
