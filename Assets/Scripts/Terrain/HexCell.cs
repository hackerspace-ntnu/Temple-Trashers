using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


[Serializable]
public struct HexCellType
{
    public Material material;
    public float elevation;
}

[ExecuteInEditMode]
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public MeshRenderer meshRenderer;

    [ReadOnly, SerializeField]
    private HexCellType _cellType;

    [SerializeField]
    private HexCell[] neighbors;

    // Is an object currently occupying (is placed on) the HexCell(tm)? This bit of code has the answers!
    [SerializeField]
    private GameObject _occupyingObject;

    private float? lastSetElevation = null;

    public HexCellType CellType
    {
        get => _cellType;
        set
        {
            _cellType = value;

            float elevationDelta = _cellType.elevation - (lastSetElevation ?? 0f);
            transform.localPosition += elevationDelta * Vector3.up;
            lastSetElevation = _cellType.elevation;

            meshRenderer.material = _cellType.material;
            // For performance reasons, cells do not cast shadows, but we wish elevated cells to do so
            meshRenderer.shadowCastingMode = _cellType.elevation > 0f ? ShadowCastingMode.On : ShadowCastingMode.Off;
        }
    }

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
