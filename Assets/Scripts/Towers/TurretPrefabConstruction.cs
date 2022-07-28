using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPrefabConstruction : AbstractTower
{
    [SerializeField]
    private GameObject towerPrefab = default;

    [SerializeField]
    private Material normalMaterial = default;

    [SerializeField]
    private Material errorMaterial = default;

    [SerializeField]
    private TutorialText tutorialText = default;

    private Renderer[] renderers = default;

    new void Awake()
    {
        base.Awake();

        renderers = GetComponentsInChildren<Renderer>();
    }

    public void Construct(HexCell targetCell, Vector3 facingDir)
    {
        GameObject tower = Instantiate(towerPrefab, targetCell.transform.position, towerPrefab.transform.rotation, targetCell.transform);
        targetCell.OccupyingObject = tower;

        TowerLogic towerLogic = tower.GetComponent<TowerLogic>();
        towerLogic.RotateFacing(facingDir);

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
