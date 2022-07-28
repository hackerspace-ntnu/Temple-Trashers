using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "TowerScriptableObject")]
public class TowerScriptableObject : ScriptableObject
{
    [SerializeField]
    private string towerName = default;

    [SerializeField]
    private int cost = default;

    [SerializeField]
    private TurretPrefabConstruction towerConstructionPrefab = default;

    public Sprite icon = default;
    public Sprite iconHighlight = default;

    public int Cost => cost;

    public TurretPrefabConstruction InstantiateConstructionTower(PlayerStateController controller)
    {
        GameObject spawnedConstructionTower = Instantiate(towerConstructionPrefab.gameObject);
        controller.PrepareTurret(spawnedConstructionTower.GetComponent<Interactable>());

        TurretPrefabConstruction turretConstruction = spawnedConstructionTower.GetComponent<TurretPrefabConstruction>();
        turretConstruction.FocusCell(controller.TargetCell);
        turretConstruction.RotateFacing(controller.transform.forward);

        return turretConstruction;
    }
}
