using System;
using System.Collections;
using UnityEngine;


[ExecuteInEditMode]
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public MeshRenderer meshRenderer;

    public int elevation = 0;

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
