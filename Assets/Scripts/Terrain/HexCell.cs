using System.Collections;
using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

    // Is an object currently occupying (is placed on) the HexCell(tm)? This bit of code has the answers!
    public bool isOccupied;
    public GameObject occupier;

    private MeshRenderer mr;
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
			transform.localPosition = position;
            ResetCell();

            // Increase the height of the highest cells
            if (Elevation == materials.Length - 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }

            // Drop the cells with the lowest elevation down
            else if (Elevation == 0)
            {
                transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
            }
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

    private void ResetCell()
	{
        if(mr == null)
        {
            mr = GetComponent<MeshRenderer>();
        }
		pertubValue = HexMetrics.SampleNoise(Position).y;
        mr.material = materials[elevation];
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