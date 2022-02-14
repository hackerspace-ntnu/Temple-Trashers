using UnityEngine;


[System.Serializable]
public class TerrainData
{
    public int xChunks;
    public int zChunks;

    public int[] elevation;
    public string[] occupier;
    public float[] occupierRotation;

    public TerrainData(HexGrid grid)
    {
        xChunks = grid.chunkCountX;
        zChunks = grid.chunkCountZ;

        elevation = new int[grid.cells.Length];
        occupier = new string[grid.cells.Length];
        occupierRotation = new float[grid.cells.Length];

        for (int i = 0; i < elevation.Length; i++)
        {
            elevation[i] = grid.cells[i].Elevation;

            if (grid.cells[i].OccupyingObject != null)
            {
                occupier[i] = GetOccupierName(grid.cells[i]);
                occupierRotation[i] = GetOccupierRotation(grid.cells[i]);
            } else
            {
                occupier[i] = "null";
                occupierRotation[i] = 0;
            }
        }
    }

    private string GetOccupierName(HexCell cell)
    {
        return cell.OccupyingObject.name;
    }

    private float GetOccupierRotation(HexCell cell)
    {
        RotatableTowerLogic rotatableTowerLogic = cell.OccupyingObject.GetComponent<RotatableTowerLogic>();
        Transform occupierTransform = rotatableTowerLogic ? rotatableTowerLogic.rotAxis : cell.OccupyingObject.transform;
        return occupierTransform.rotation.eulerAngles.y;
    }
}
