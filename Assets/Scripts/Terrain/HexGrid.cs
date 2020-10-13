using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{   
    public int chunkCountX = 4, chunkCountZ = 3;

    public HexGridChunk chunkPrefab;

    private int cellCountX, cellCountZ;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.gray;

    public HexCell cellPrefab;
    public Text cellLabelPrefab;


    HexCell[] cells;
    HexGridChunk[] chunks;
    void Awake()
    {
        cellCountX = chunkCountX * HexMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

        CreateChunks();
        CreateCells();
    }

    void CreateChunks()
    {
        chunks = new HexGridChunk[chunkCountX * chunkCountZ];

        for(int z = 0, i=0; z < chunkCountZ; z++)
        {
            for(int x = 0; x < chunkCountX; x++)
            {
                HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
            }
        }
    }
    void CreateCells()
    {
        cells = new HexCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        if(i % 2 == 0)
        {
            cell.color = defaultColor;
        }
        else
        {
            cell.color = touchedColor;
        }

        // Set neighbors
        if (x > 0) // Set the E - W neighbor
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if(z > 0)
        {
            if((z & 1) == 0) // Set the NW-SW neighbor
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
            }
            if(x > 0)       // Set the NE to SW neighbor on even rows
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
            }
        }
        else
        {
            cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
            if (x < cellCountX - 1) // Set the NE to SW on odd rows
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
            }
        }
        

        // Position labeling with Text UI
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.rectTransform.sizeDelta = new Vector2(2f * HexMetrics.innerRadius, 1.5f * HexMetrics.outerRadius);
        label.text = cell.coordinates.ToStringOnSeperateLines();

        AddCellToChunk(x, z, cell);
    }

    void AddCellToChunk(int x, int z, HexCell cell)
    {
        int chunkX = x / HexMetrics.chunkSizeX;
        int chunkZ = z / HexMetrics.chunkSizeZ;
        HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * HexMetrics.chunkSizeX;
        int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
        chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
    }
}
