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

    [ReadOnly, SerializeField]
    private HexCellType _cellType;

    [ReadOnly, SerializeField]
    private HexCell[] neighbors = default;

    // Is an object currently occupying (is placed on) the HexCell(tm)? This bit of code has the answers!
    [SerializeField]
    private GameObject _occupyingObject;

    private MeshRenderer meshRenderer;

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

    public float dstToBase;

    /// <returns>
    /// The <c>GameObject</c> placed on top of this cell.
    /// <br/>
    /// Note: this might be an already destroyed <c>GameObject</c>!
    /// </returns>
    public GameObject OccupyingObject
    {
        get => _occupyingObject;
        set
        {
            if (value && IsOccupied)
            {
                Destroy(value);
                Debug.LogError($"Cannot place {value} on cell at {coordinates}, as it's already occupied by {_occupyingObject}!");
                return;
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

    /// <returns>
    /// <c>false</c> if the cell is occupied by another object, or if it's too close to the player base or another tower;
    /// <c>true</c> otherwise.
    /// </returns>
    public bool CanPlaceTowerOnCell
    {
        get
        {
            if (IsOccupied)
                return false;

            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
            {
                GameObject neighboringOccupyingObject = GetNeighbor(direction).OccupyingObject;
                if (!neighboringOccupyingObject)
                    continue;
                // If a neighboring cell has a tower or the player base on top of it, it's not allowed to place a tower on this cell,
                // as they would be too close to each other
                if (neighboringOccupyingObject.GetComponent<TowerLogic>()
                    || neighboringOccupyingObject.GetComponent<BaseController>())
                {
                    return false;
                }
            }

            return true;
        }
    }

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        dstToBase = Vector3.Distance(transform.position, BaseController.Singleton.transform.position);
    }

    public GameObject InstantiatePrefabOnCell(GameObject prefab)
    {
        if (IsOccupied)
        {
            Debug.LogError($"Cannot place {prefab} on cell at {coordinates}, as it's already occupied by {OccupyingObject}!");
            return null;
        }

        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        OccupyingObject = obj;
        return obj;
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
