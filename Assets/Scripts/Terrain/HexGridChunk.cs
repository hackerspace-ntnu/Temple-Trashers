using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class HexGridChunk : MonoBehaviour
{
    //HexCell[] cells;

    HexMesh hexmesh;
    Canvas gridCanvas;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexmesh = GetComponentInChildren<HexMesh>();

        //cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
    }

    private void Start()
    {
        //hexmesh.Triangulate(cells);
    }

    public void AddCell(int index, HexCell cell)
    {
        //cells[index] = cell;
        cell.transform.SetParent(transform, false);
        cell.uiRect.SetParent(gridCanvas.transform, false);

    }
}

