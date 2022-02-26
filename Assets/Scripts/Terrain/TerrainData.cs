using System;
using UnityEngine;


[Serializable]
public struct HexCellData
{
    public HexCellType cellType;
    public string occupier;
    public float occupierRotation;
}

[Serializable]
public class TerrainData
{
    public int xChunks;
    public int zChunks;

    public HexCellData[] hexCells;

    public TerrainData(HexGrid grid)
    {
        xChunks = grid.chunkCountX;
        zChunks = grid.chunkCountZ;

        int numCells = grid.cells.Length;
        hexCells = new HexCellData[numCells];

        for (int i = 0; i < numCells; i++)
        {
            HexCell hexCell = grid.cells[i];
            HexCellData hexCellData = new HexCellData();
            hexCellData.cellType = hexCell.CellType;

            if (hexCell.OccupyingObject)
            {
                hexCellData.occupier = GetOccupierName(hexCell);
                hexCellData.occupierRotation = GetOccupierRotation(hexCell);
            } else
            {
                hexCellData.occupier = "null";
                hexCellData.occupierRotation = 0f;
            }

            hexCells[i] = hexCellData;
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
