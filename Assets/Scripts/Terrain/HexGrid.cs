using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;

[ExecuteInEditMode]
public class HexGrid : MonoBehaviour {

	private int cellCountX, cellCountZ;

	public int chunkCountX = 4;
	public int chunkCountZ = 3;

	public HexGridChunk chunkPrefab;

	public HexCell cellPrefab;
	//public Text cellLabelPrefab;
    
	HexGridChunk[] chunks;
	public HexCell[] cells;
	public HexCell[] edgeCells;

	public bool recalculate;

	[Header("Noise")]
	public Texture2D noise;

    [Header("Noise Scale")]
    public float noiseScale;

    [Header("Terrain Animation")]
    public bool enabled = false;
    [SerializeField]
    private Transform hq;
    public float scale = 0.1f;
    public float speed = 0.5f;

	private void OnEnable()
    {
		HexMetrics.noiseSource = noise;
        HexMetrics.noiseScale = noiseScale;
	}
	private void Start() {
		cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		CreateChunks();
		CreateCells();

		edgeCells = GetEdgeCells();
        hq = GameObject.FindGameObjectWithTag("Base").transform;
    }

    private void Update()
    {
        if (recalculate)
        {
			recalculate = false;

			cellCountX = chunkCountX * HexMetrics.chunkSizeX;
			cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

			CreateChunks();
			CreateCells();

			edgeCells = GetEdgeCells();
		}

        // Terain floating job
        if (enabled)
        {
            NativeArray<float3> cellPosArray = new NativeArray<float3>(cells.Length, Allocator.TempJob);
            NativeArray<float> cellElevation = new NativeArray<float>(cells.Length, Allocator.TempJob);
            NativeArray<float> result = new NativeArray<float>(cells.Length, Allocator.TempJob);
            for (int i = 0; i < cells.Length; i++)
            {
                cellPosArray[i] = cells[i].transform.position;
            }
            CellAnimJob cellAnim = new CellAnimJob
            {
                time = Time.realtimeSinceStartup,
                targetPos = hq.position,
                currPosArray = cellPosArray,
                scale = scale,
                speed = speed,
                elevationStep = HexMetrics.elevationStep,
                elevation = cellElevation,
                adjustment = result,
            };

            JobHandle jobHandle = cellAnim.Schedule(cells.Length, 100);
            jobHandle.Complete();

            // Apply the values
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].transform.position = new Vector3(
                    cells[i].transform.position.x,
                    result[i],
                    cells[i].transform.position.z);
            }
            cellPosArray.Dispose();
            cellElevation.Dispose();
            result.Dispose();
        }
    }

	void CreateChunks()
	{
		// Remove previous chunks
		chunks = GetComponentsInChildren<HexGridChunk>();
		foreach (HexGridChunk chunk in chunks)
		{
			DestroyImmediate(chunk.gameObject);
		}

		chunks = new HexGridChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++)
		{
			for (int x = 0; x < chunkCountX; x++)
			{
				HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
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
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
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
		/*
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.rectTransform.sizeDelta =
			new Vector2(HexMetrics.outerRadius * 2, HexMetrics.innerRadius * 2);
		label.text = cell.coordinates.ToStringOnSeparateLines();
		cell.uiRect = label.rectTransform;
		*/

		cell.Elevation = Mathf.FloorToInt(HexMetrics.SampleNoise(cell.transform.localPosition).y * cell.materials.Length * 1.1f);

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
		for (int x = 0; x < cellCountZ-1; x++)
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
		for (int x = 0; x < cellCountZ-1; x++)
		{
			edgeCells[i] = cells[currentPos.x + currentPos.y * cellCountX];
			i++;
			currentPos = currentPos + new Vector2Int(0, -1);
		}
		foreach (HexGridChunk chunk in chunks)
        {
			chunk.Refresh();
        }
		return edgeCells;
    }
}

public struct CellAnimJob : IJobParallelFor{
	public NativeArray<float3> currPosArray;
    public NativeArray<float> elevation;
	[ReadOnly] public float3 targetPos;
	[ReadOnly] public float time;
    [ReadOnly] public float scale;
    [ReadOnly] public float speed;
    [ReadOnly] public float elevationStep;
    
    public NativeArray<float> adjustment;
	
    public void Execute(int index){
	    // Animated terrain waves
		// Distance between cell and the base
       	float dst = math.sqrt(
            (targetPos.x - currPosArray[index].x) * (targetPos.x - currPosArray[index].x) +
            (targetPos.z - currPosArray[index].z) * (targetPos.z - currPosArray[index].z));

        adjustment[index] = math.sin(-2 * math.PI * time * speed + dst) * scale;
	}
}
