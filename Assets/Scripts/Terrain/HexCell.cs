using System.Collections;
using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

	private int elevation;

	//public RectTransform uiRect;
	private Vector3 position;
	private Vector3 newPos;
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
			position.y = value * HexMetrics.elevationStep;
			position.y +=
				(HexMetrics.SampleNoise(position).y * 2f - 1f) *
				HexMetrics.elevationPerturbStrength;
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
	public bool placedTurret;

	public Material[] materials;


	public float animationScale = 10f;

	[SerializeField]
	HexCell[] neighbors = null;

	private Transform hq;

    private void Start()
	{
		pertubValue = HexMetrics.SampleNoise(Position).y;
		GetComponent<MeshRenderer>().material = materials[elevation];
		hq = GameObject.Find("Base").transform;
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
}