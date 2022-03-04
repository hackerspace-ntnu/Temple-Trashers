using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteInEditMode]
public class HexGrid : MonoBehaviour
{
    public static HexGrid Singleton { get; private set; }

    private int cellCountX;
    private int cellCountZ;

    [Header("Structural Variables")]
    [Range(1, 20)]
    public int chunkCountX = 4;

    [Range(1, 20)]
    public int chunkCountZ = 3;

    [SerializeField]
    private Transform chunkPrefab;

    [SerializeField]
    private HexCell cellPrefab;

    private GameObject playerBase;

    [Header("Terrain Data")]
    private Transform[] chunks;

    [HideInInspector]
    public HexCell[] cells;

    public HexCell[] edgeCells;

    [Header("Scenery Variables")]
    [SerializeField]
    private GameObject[] sceneryObjects;

    [SerializeField]
    private bool mountainBorder;

    [SerializeField]
    private bool treeBorder;

    [Header("Noise")]
    [SerializeField]
    private Texture2D noise;

    [Header("Noise Scale")]
    [SerializeField]
    private float noiseScale;

    // Custom inspector variables
    [Header("Cell Variables")]
    [SerializeField]
    private HexCellType[] cellTypes;

    [Header("Savekey")]
    [SerializeField]
    private GameObject[] towerPrefabs;

    private IDictionary<string, GameObject> nameToGameObject;
    private IDictionary<GameObject, string> gameObjectToName;

    void OnEnable()
    {
        HexMetrics.noiseScale = noiseScale;
    }

    void Awake()
    {
        #region Singleton boilerplate

        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Debug.LogWarning($"There's more than one {Singleton.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        Singleton = this;

        #endregion Singleton boilerplate

        playerBase = GameObject.FindGameObjectWithTag("Base");

        // Calculate borders for the terrain
        cellCountX = chunkCountX * HexMetrics.CHUNK_SIZE_X;
        cellCountZ = chunkCountZ * HexMetrics.CHUNK_SIZE_Z;

        nameToGameObject = new Dictionary<string, GameObject>();
        gameObjectToName = new Dictionary<GameObject, string>();

        IEnumerable<GameObject> allGameObjectArrays = towerPrefabs.Concat(sceneryObjects);
        foreach (GameObject obj in allGameObjectArrays)
        {
            string gameObjectName = $"{obj.name}(Clone)";
            nameToGameObject.Add(gameObjectName, obj);
            gameObjectToName.Add(obj, gameObjectName);
        }
    }

    /// <summary>
    /// Rebuild the terrain from scratch
    /// </summary>
    public void RebuildTerrain()
    {
        // Calculate borders
        cellCountX = chunkCountX * HexMetrics.CHUNK_SIZE_X;
        cellCountZ = chunkCountZ * HexMetrics.CHUNK_SIZE_Z;

        // Create the terrain
        CreateChunks();
        CreateCells();

        PlacePlayerBase();

        PlaceSceneryObjects();
    }

    private void PlacePlayerBase()
    {
        // Place the base in the middle of the terrain
        Vector3 firstCellPos = cells[0].transform.position;
        Vector3 lastCellPos = cells[cells.Length - 1].transform.position;
        Vector3 basePos = (lastCellPos - firstCellPos) / 2f;
        HexCell centerCell = GetCell(basePos);

        playerBase.transform.position = centerCell.transform.position;
        centerCell.OccupyingObject = playerBase;
    }

    // Create the chunks to better organize the cell data
    private void CreateChunks()
    {
        // Remove previous chunks
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);

        chunks = new Transform[chunkCountX * chunkCountZ];

        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                Transform chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
                chunk.name = $"Chunk ({x}, {z})";
                chunk.transform.position -= new Vector3(
                    HexMetrics.INNER_RADIUS * cellCountX,
                    0,
                    HexMetrics.OUTER_RADIUS * cellCountZ * 1.5f / 2f
                );
            }
        }
    }

    // Creates an array of all cells and fills it out with individual cells
    private void CreateCells()
    {
        cells = new HexCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
                CreateCell(x, z, i++);
        }

        edgeCells = GetEdgeCells();
    }

    // Returns the HexCell at a given position
    public HexCell GetCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        position -= cells[0].transform.position;
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;

        return cells[index];
    }

    private void CreateCell(int x, int z, int i)
    {
        // Determine the position of the cell
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.INNER_RADIUS * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.OUTER_RADIUS * 1.5f);

        // Instantiate and set up the cell
        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.localPosition = position;

        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        // Assign the neighbors of the cell
        if (x > 0)
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);

        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                if (x > 0)
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
            } else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                if (x < cellCountX - 1)
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
            }
        }

        // Add cell to correlating chunk
        int chunkX = x / HexMetrics.CHUNK_SIZE_X;
        int chunkZ = z / HexMetrics.CHUNK_SIZE_Z;

        cell.transform.SetParent(chunks[chunkX + chunkZ * chunkCountX]);

        SetTypeForCell(cell);
    }

    /// <summary>
    /// Adjust a cells transform and material information based off the elevation integer.
    /// </summary>
    /// <param name="cell">The target cell</param>
    private void SetTypeForCell(HexCell cell)
    {
        float noiseValue = HexMetrics.SampleNoise(cell.transform.localPosition, noise).y; // will be between 0.0 and 1.0 (both inclusive)
        int cellTypeIndex = Mathf.FloorToInt(noiseValue * cellTypes.Length * 0.99f); // will be between 0 and the last index of `cellTypes`
        cell.CellType = cellTypes[cellTypeIndex];
    }

    /// <summary>
    /// Returns an array containing the cells on the edge of the map.
    /// </summary>
    /// <returns></returns>
    public HexCell[] GetEdgeCells()
    {
        HexCell[] edgeCells = new HexCell[2 * cellCountX + 2 * cellCountZ - 4];
        // Get the lower edge
        int i = 0;
        Vector2Int currentPos = new Vector2Int(0, 0);

        for (int x = 0; x < cellCountX - 1; x++)
        {
            edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
            i++;
            currentPos += new Vector2Int(1, 0);
        }

        for (int x = 0; x < cellCountZ - 1; x++)
        {
            edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
            i++;
            currentPos += new Vector2Int(0, 1);
        }

        for (int x = 0; x < cellCountX - 1; x++)
        {
            edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
            i++;
            currentPos += new Vector2Int(-1, 0);
        }

        for (int x = 0; x < cellCountZ - 1; x++)
        {
            edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
            i++;
            currentPos += new Vector2Int(0, -1);
        }

        return edgeCells;
    }

    /// <summary>
    /// Creates the map decorations
    /// </summary>
    private void PlaceSceneryObjects()
    {
        if (sceneryObjects.Length == 0)
        {
            Debug.LogError("No scenery objects have been assigned!");
            return;
        }

        // Code from https://stackoverflow.com/a/3188804
        HexCellType tallestCellType = cellTypes.Aggregate((t1, t2) => t1.elevation > t2.elevation ? t1 : t2);
        PlaceSceneryOnMapEdge(tallestCellType);
        PlaceSceneryOnPlayArea(tallestCellType);
    }

    private void PlaceSceneryOnMapEdge(HexCellType tallestCellType)
    {
        // Adding a wall around the map
        foreach (HexCell cell in edgeCells)
        {
            if (mountainBorder)
                cell.CellType = tallestCellType;

            if (treeBorder)
            {
                GameObject sceneryObj = cell.InstantiatePrefabOnCell(sceneryObjects[0]);
                float yRotation = Random.Range(0f, 360f);
                sceneryObj.transform.Rotate(0, yRotation, 0);
            }
        }
    }

    private void PlaceSceneryOnPlayArea(HexCellType tallestCellType)
    {
        foreach (HexCell cell in cells)
        {
            float elevation = cell.CellType.elevation;
            if (Random.Range(0, 100) > 10f
                || elevation < 0
                || elevation >= tallestCellType.elevation
                || cell.IsOccupied)
                continue;

            int sceneryIndex = Random.Range(0, sceneryObjects.Length);
            GameObject sceneryObject = cell.InstantiatePrefabOnCell(sceneryObjects[sceneryIndex]);
            float yRotation = Random.Range(0f, 360f);
            sceneryObject.transform.Rotate(0, yRotation, 0);
        }
    }

    public void SaveTerrain()
    {
        SaveSystem.SaveTerrain(this);
    }

    public void LoadTerrain()
    {
        TerrainData data = SaveSystem.LoadTerrain();
        if (data == null)
            return;

        chunkCountX = data.xChunks;
        chunkCountZ = data.zChunks;

        // Create a basic terrain
        CreateChunks();
        CreateCells();

        PlacePlayerBase();

        // Update all cells according to stored data
        for (int i = 0; i < cells.Length; i++)
        {
            HexCellData hexCellData = data.hexCells[i];
            cells[i].CellType = hexCellData.cellType;

            if (hexCellData.occupier == "null" || !nameToGameObject.ContainsKey(hexCellData.occupier))
                continue;

            GameObject sceneryObject = cells[i].InstantiatePrefabOnCell(nameToGameObject[hexCellData.occupier]);
            if (sceneryObject.GetComponent<RotatableTowerLogic>() is RotatableTowerLogic rotatableTowerLogic)
            {
                Quaternion rotation = rotatableTowerLogic.rotAxis.rotation;
                rotatableTowerLogic.rotAxis.rotation = rotation * Quaternion.Euler(0f, hexCellData.occupierRotation, 0f);
            } else
            {
                sceneryObject.transform.rotation = Quaternion.Euler(0f, hexCellData.occupierRotation, 0f);
            }
        }
    }
}
