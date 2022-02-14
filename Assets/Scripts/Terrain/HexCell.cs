using System;
using System.Collections;
using UnityEngine;


[ExecuteInEditMode]
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public MeshRenderer meshRenderer;

    private int _elevation = 0;

    public int Elevation
    {
        get => _elevation;
        set
        {
            _elevation = value;

            if (_elevation == materials.Length - 1)
            {
                // Increase the height of the highest cells
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            } else if (_elevation == 0)
            {
                // Drop the cells with the lowest elevation down
                transform.position = new Vector3(transform.position.x, -0.3f, transform.position.z);
            }

            meshRenderer.material = materials[_elevation];
        }
    }

    public Material[] materials;

    [SerializeField]
    private HexCell[] neighbors;

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
    /// (Destroying a game object that was occupying this cell, will make this property return <c>true</c>
    /// - without having to change the value of <c>OccupyingObject</c>.)
    /// </returns>
    public bool IsOccupied => OccupyingObject;

    void Awake()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        // Invoke the logic in Elevation's setter initially
        Elevation = Elevation;
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}
