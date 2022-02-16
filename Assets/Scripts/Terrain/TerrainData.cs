using System;
using UnityEngine;


[Serializable]
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

        int numCells = grid.cells.Length;
        elevation = new int[numCells];
        occupier = new string[numCells];
        occupierRotation = new float[numCells];

        for (int i = 0; i < numCells; i++)
        {
            HexCell hexCell = grid.cells[i];
            elevation[i] = hexCell.elevation;

            if (hexCell.OccupyingObject)
            {
                occupier[i] = GetOccupierName(hexCell);
                occupierRotation[i] = GetOccupierRotation(hexCell);
            } else
            {
                occupier[i] = "null";
                occupierRotation[i] = 0f;
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
