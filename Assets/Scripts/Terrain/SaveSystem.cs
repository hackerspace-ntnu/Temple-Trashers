using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    private static string DataPath => $"{Application.persistentDataPath}/terrain.data";

    public static void SaveTerrain(HexGrid grid)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(DataPath, FileMode.Create);

        TerrainData data = new TerrainData(grid);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static TerrainData LoadTerrain()
    {
        if (File.Exists(DataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(DataPath, FileMode.Open);

            TerrainData data = formatter.Deserialize(stream) as TerrainData;

            stream.Close();
            return data;
        } else
        {
            Debug.LogError($"Terrain data not found at {DataPath}");
            return null;
        }
    }
}
