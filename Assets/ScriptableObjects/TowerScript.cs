﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TowerScriptableObject")]
public class TowerScript : ScriptableObject
{
   public string towerName;
   public int cost;
   public GameObject tower;
   public Sprite icon;
   public Sprite iconHighlight;
}