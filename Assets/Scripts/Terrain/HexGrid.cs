using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class HexGrid : MonoBehaviour {

	private int cellCountX, cellCountZ;

    public bool recalculate = true;

    [Header("Structural Variables")]
    [Range(1,10)]
	public int chunkCountX = 4;
    [Range(1,10)]
	public int chunkCountZ = 3;

	public Transform chunkPrefab;
	public HexCell cellPrefab;
    
    [Header("Terrain Data")]
	Transform[] chunks;
	public HexCell[] cells;
	public HexCell[] edgeCells;

    [Header("Decoration Variables")]
    public GameObject[] decor;
    public bool mountainBorder;
    public bool treeBorder;

    [Header("Noise")]
	public Texture2D noise;

    [Header("Noise Scale")]
    public float noiseScale;

    [Header("Savekey")]
    public GameObject[] towers;
    public IDictionary<string, GameObject> nameToGO;
    public IDictionary<GameObject, string> GOToName;

    private void OnEnable()
    {
		HexMetrics.noiseSource = noise;
        HexMetrics.noiseScale = noiseScale;
	}
    
    
	private void Awake() {
        // Calculate borders for the terrain
        cellCountX = chunkCountX * HexMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

        nameToGO = new Dictionary<string, GameObject>();
        GOToName = new Dictionary<GameObject, string>();

        foreach (GameObject g in decor)
        {
            nameToGO.Add(g.name + "(Clone)", g);
            GOToName.Add(g, g.name + "(Clone)");
        }
        foreach (GameObject g in towers)
        {
            nameToGO.Add(g.name + "(Clone)", g);
            GOToName.Add(g, g.name + "(Clone)");
        }

        LoadTerrain();
    }

    private void Update()
    {
        if (recalculate)
        {
			recalculate = false;
            RebuildTerrain();
        }
    }

    /// <summary>
    /// Rebuild the terrain from scratch
    /// </summary>
    public void RebuildTerrain()
    {
        // Calculate borders
        cellCountX = chunkCountX * HexMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

        // Create the terrain
        CreateChunks();
        CreateCells();

        // Put the base in the middle of the terrain
        Vector3 basePos = (cells[cells.Length - 1].transform.position - cells[0].transform.position) / 2;
        HexCell centreCell = GetCell(basePos);

        GameObject.FindGameObjectWithTag("Base").transform.position = centreCell.transform.position;
        centreCell.OccupyingObject = GameObject.FindGameObjectWithTag("Base");

        CreateDecorations();
    }

	void CreateChunks()
	{
		// Remove previous chunks
		while(transform.childCount > 0)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}

		chunks = new Transform[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++)
		{
			for (int x = 0; x < chunkCountX; x++)
			{
				Transform chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
                chunk.name = "Chunk (" + x.ToString() + ", " + z.ToString() + ")"; 
                chunk.transform.position -= new Vector3(
                    HexMetrics.innerRadius * cellCountX,
                    0,
                    HexMetrics.outerRadius * cellCountZ * 1.5f / 2f
                    );
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

        edgeCells = GetEdgeCells();
    }

    // Returns the HexCell at a given position
	public HexCell GetCell (Vector3 position) {
        
		position = transform.InverseTransformPoint(position);
        position -= cells[0].transform.position;
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;

        return cells[index];
	}


	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

		if (x > 0) {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
		if (z > 0) {
			if ((z & 1) == 0) {
				cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
				if (x > 0) {
					cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
				}
			}
			else {
				cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
				if (x < cellCountX - 1) {
					cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
				}
			}
		}

        cell.Elevation = Mathf.FloorToInt(HexMetrics.SampleNoise(cell.transform.localPosition).y * cell.materials.Length * 1.1f);

        // Add cell to correlating chunk
        int chunkX = x / HexMetrics.chunkSizeX;
        int chunkZ = z / HexMetrics.chunkSizeZ;

        cell.transform.SetParent(chunks[chunkX + chunkZ * chunkCountX]);
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

		for(int x = 0; x < cellCountX - 1; x++)
        {
			edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
			i++;
			currentPos = currentPos + new Vector2Int(1, 0);
        }
		for (int x = 0; x < cellCountZ - 1; x++)
		{
			edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
			i++;
			currentPos = currentPos + new Vector2Int(0, 1);
		}
		for (int x = 0; x < cellCountX - 1; x++)
		{
			edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
			i++;
			currentPos = currentPos + new Vector2Int(-1, 0);
		}
		for (int x = 0; x < cellCountZ - 1; x++)
		{
			edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
			i++;
			currentPos = currentPos + new Vector2Int(0, -1);
		}

		return edgeCells;
    }

    /// <summary>
    /// Creates the map decorations
    /// </summary>
    void CreateDecorations()
    {
        // The cells inbetween the min and max elevations are areas where we can place decorations.
        int minElevation = 1;
        int maxElevation = cellPrefab.materials.Length - 1;

        if (decor.Length == 0) {
            Debug.LogError("No decorations have been assgined");
            return;
        }
        
        // Adding a wall around the map
        foreach (HexCell c in edgeCells)
        {
            if (mountainBorder)
            {
                c.Elevation = maxElevation;
            }
            if (treeBorder)
            {
                GameObject decoration = Instantiate(decor[0], c.transform.position, Quaternion.identity);
                decoration.transform.SetParent(c.transform);
                decoration.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
                c.OccupyingObject = decoration;
            }
        }

        foreach(HexCell cell in cells)
        {
            int h = cell.Elevation;

            if (Random.Range(0, 100) <= 10f && h >= minElevation && h < maxElevation)
            {
                if (!cell.IsOccupied)
                {
                    int decorIndex = Random.Range(0, decor.Length);
                    GameObject decoration = Instantiate(decor[decorIndex], cell.transform.position, Quaternion.identity);
                    decoration.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
                    decoration.transform.SetParent(cell.transform);
                    cell.OccupyingObject = decoration;
                }
            }
        }
    }

    public void SaveTerrain()
    {
        SaveSystem.SaveTerrain(this);
    }

    public void LoadTerrain()
    {
        
        TerrainData data = SaveSystem.LoadTerrain(this);

        chunkCountX = data.xChunks;
        chunkCountZ = data.zChunks;

        // Create a basic terrain
        CreateChunks();
        CreateCells();

        // Put the base in the middle of the terrain
        Vector3 basePos = (cells[cells.Length - 1].transform.position - cells[0].transform.position) / 2;
        HexCell centreCell = GetCell(basePos);

        GameObject.FindGameObjectWithTag("Base").transform.position = centreCell.transform.position;
        centreCell.OccupyingObject = GameObject.FindGameObjectWithTag("Base");

        // Update all cells according to stored data
        for(int i = 0; i < cells.Length; i++)
        {
            cells[i].Elevation = data.elevation[i];

            if (data.occupier[i] != "null" && nameToGO.ContainsKey(data.occupier[i]))
            {
                GameObject decoration = Instantiate(nameToGO[data.occupier[i]], cells[i].transform.position, Quaternion.identity);
                if (decoration.GetComponent<RotatableTowerLogic>() != null)
                {
                    Quaternion rotation = decoration.GetComponent<RotatableTowerLogic>().rotAxis.rotation;
                    decoration.GetComponent<RotatableTowerLogic>().rotAxis.rotation = rotation * Quaternion.Euler(0f, data.occupierRotation[i], 0f);
                }
                else
                {
                    decoration.transform.rotation = Quaternion.Euler(0f, data.occupierRotation[i], 0f);
                }

                decoration.transform.SetParent(cells[i].transform);
                cells[i].OccupyingObject = decoration;
            }
        }
    }
}