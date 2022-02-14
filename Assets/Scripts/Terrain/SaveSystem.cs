using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem { 
    
    public static void SaveTerrain(HexGrid grid)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/terrain.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        TerrainData data = new TerrainData(grid);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static TerrainData LoadTerrain(HexGrid grid)
    {
        string path = Application.persistentDataPath + "/terrain.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TerrainData data = formatter.Deserialize(stream) as TerrainData;

            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Terrain data not found at " + path);
            return null;
        }
    }
}
