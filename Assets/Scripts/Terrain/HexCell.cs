using System.Collections;
using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

    // Is an object currently occupying (is placed on) the HexCell(tm)? This bit of code has the answers!
    public bool isOccupied;
    public GameObject occupier;

    [SerializeField]
    private MeshRenderer mesh;
	//public RectTransform uiRect;
	private Vector3 position;
	private Vector3 newPos;
	private int elevation;
    public int Elevation
    {
        get
        {
			return elevation;
        }
        set
        {
			elevation = value;
			position = transform.localPosition;
			//position.y = value * HexMetrics.elevationStep;
			//position.y += (HexMetrics.SampleNoise(position).y * 2f - 1f) * HexMetrics.elevationPerturbStrength;
			transform.localPosition = position;
        }
    }
	public float pertubValue;

	public Vector3 Position
    {
        get
        {
			return transform.localPosition;
        }
    }

	public Material[] materials;


	public float animationScale = 10f;

	[SerializeField]
	HexCell[] neighbors;

	private Transform hq;

    void Awake()
	{
        if(mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
        }

        hq = GameObject.Find("Base").transform;
    }

    void Start()
    {
		pertubValue = HexMetrics.SampleNoise(Position).y;
        mesh.material = materials[elevation];
    }

    public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}

	public HexEdgeType GetEdgeType(HexDirection direction)
	{
		return HexMetrics.GetEdgeType(elevation, neighbors[(int)direction].elevation);
	}

	public HexEdgeType GetEdgeType(HexCell otherCell)
    {
		return HexMetrics.GetEdgeType(elevation, otherCell.elevation);
    }

    public void SetTower(GameObject tower)
    {
        isOccupied = true;
        occupier = tower;
    }
}
