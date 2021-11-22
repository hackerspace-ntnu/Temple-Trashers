using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HexGridChunk : MonoBehaviour
{
    HexCell[] cells;

    HexMesh hexmesh;
    Canvas gridCanvas;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexmesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
    }

    void Start()
    {
        hexmesh.Triangulate(cells);
    }

    void Update()
    {
        if(cells.Length == 0)
        {
            cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
        }
    }

    public void AddCell(int index, HexCell cell)
    {
        cells[index] = cell;
        cell.transform.SetParent(transform, false);
        //cell.Elevation = Mathf.RoundToInt(HexMetrics.SampleNoise(cell.coordinates).x*cell.materials.Length);
        //cell.uiRect.SetParent(gridCanvas.transform, false);

    }

    public void Refresh()
    {
        hexmesh.Triangulate(cells);
    }
}
