using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPrefabConstruction : Interactable
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Material normalMaterial;

    [SerializeField]
    private Material errorMaterial;

    [SerializeField]
    private TowerScriptableObject _towerScriptableObject;

    [SerializeField]
    private TutorialText tutorialText;

    private Renderer[] renderers;

    public TowerScriptableObject TowerScriptableObject => _towerScriptableObject;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void Construct(HexCell targetCell)
    {
        GameObject tower = Instantiate(towerPrefab, targetCell.transform.position, towerPrefab.transform.rotation, targetCell.transform);
        targetCell.OccupyingObject = tower;
        Destroy(gameObject);
    }

    public override void Interact(PlayerStateController player)
    {}

    public void FocusCell(HexCell targetCell)
    {
        transform.position = targetCell.transform.position;
        SetMaterial(targetCell.CanPlaceTowerOnCell ? normalMaterial : errorMaterial);
        tutorialText.SetButton(Direction.SOUTH, targetCell.CanPlaceTowerOnCell);
    }

    private void SetMaterial(Material material)
    {
        foreach (Renderer renderer in renderers)
        {
            if (!renderer.enabled || renderer.CompareTag("UI"))
                continue;

            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
                newMaterials[i] = material;

            renderer.materials = newMaterials;
        }
    }
}
