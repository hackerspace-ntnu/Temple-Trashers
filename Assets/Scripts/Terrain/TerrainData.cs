using System.Collections;
using System.Collections.Generic;
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

        for(int i = 0; i < elevation.Length; i++)
        {
            elevation[i] = grid.cells[i].Elevation;

            if (grid.cells[i].OccupyingObject != null)
            {
                occupier[i] = GetOccupierName(grid, grid.cells[i]);
                occupierRotation[i] = GetOccupierRotation(grid, grid.cells[i]);
            }
            else
            {
                occupier[i] = "null";
                occupierRotation[i] = 0;
            }
            
        }
    }

    string GetOccupierName(HexGrid grid, HexCell cell)
    {
        return cell.OccupyingObject.name;
    }

    float GetOccupierRotation(HexGrid grid, HexCell cell)
    {
        if (cell.OccupyingObject.GetComponent<RotatableTowerLogic>() != null)
        {
            return cell.OccupyingObject.GetComponent<RotatableTowerLogic>().rotAxis.transform.rotation.eulerAngles.y;
        }
        else
        {
            return cell.OccupyingObject.transform.rotation.eulerAngles.y;
        }
    }
}
