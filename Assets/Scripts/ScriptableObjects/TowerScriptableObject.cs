using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "TowerScriptableObject")]
public class TowerScriptableObject : ScriptableObject
{
    public string towerName;
    public int cost;
    public TurretPrefabConstruction towerConstructionPrefab;
    public Sprite icon;
    public Sprite iconHighlight;

    public TurretPrefabConstruction InstantiateConstructionTower(PlayerStateController controller)
    {
        GameObject spawnedConstructionTower = Instantiate(towerConstructionPrefab.gameObject);
        controller.PrepareTurret(spawnedConstructionTower.GetComponent<Interactable>());
        return spawnedConstructionTower.GetComponent<TurretPrefabConstruction>();
    }
}
