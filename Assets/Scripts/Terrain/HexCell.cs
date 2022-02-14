using System;
using System.Collections;
using UnityEngine;
using Unity.Mathematics;


[ExecuteInEditMode]
public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;
    public MeshRenderer mr;

    private Vector3 position;
    private Vector3 newPos;
    private int elevation = 0;

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

            // Increase the height of the highest cells
            if (Elevation == materials.Length - 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }

            // Drop the cells with the lowest elevation down
            else if (Elevation == 0)
            {
                transform.position = new Vector3(transform.position.x, -0.3f, transform.position.z);
            }

            mr.material = materials[elevation];
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

    [SerializeField]
    HexCell[] neighbors;

    // Is an object currently occupying (is placed on) the HexCell(tm)? This bit of code has the answers!
    [SerializeField]
    private GameObject _occupyingObject;

    public GameObject OccupyingObject
    {
        get => _occupyingObject;
        set
        {
            if (value && IsOccupied)
            {
                Destroy(value);
                throw new ArgumentException(
                    $"Cannot place {value} on cell at {coordinates}, as it's already occupied by {_occupyingObject}!"
                );
            }
            _occupyingObject = value;
        }
    }

    /// <returns>
    /// Whether this cell has a game object placed on it or not.
    /// <br/><br/>
    /// (Destroying a game object that was occupying this cell, will make this property return `true`
    /// - without having to change the value of `OccupyingObject`.)
    /// </returns>
    public bool IsOccupied => OccupyingObject;

    void Awake()
    {
        if (mr == null)
        {
            mr = GetComponentInChildren<MeshRenderer>();
        }
        Elevation = elevation;
    }

    public HexCell GetNeighbor (HexDirection direction) {
        return neighbors[(int)direction];
    }

    public void SetNeighbor (HexDirection direction, HexCell cell) {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}
