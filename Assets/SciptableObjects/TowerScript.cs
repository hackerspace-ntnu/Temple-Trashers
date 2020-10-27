using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower")]
public class TowerScript : ScriptableObject
{
   public string towerName;
   public int cost;
   public GameObject tower;
}
