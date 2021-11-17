﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TowerScriptableObject")]
public class TowerScript : ScriptableObject
{
    public string towerName;
    public int cost;
    public TurretPrefabConstruction towerConstructionPrefab;
    public Sprite icon;
    public Sprite iconHighlight;
}
