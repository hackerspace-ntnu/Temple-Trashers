using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractTower : Interactable
{
    [SerializeField]
    protected TowerScriptableObject _towerScriptableObject = default;

    public TowerScriptableObject TowerScriptableObject => _towerScriptableObject;
}
