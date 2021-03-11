using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tower")]
public class TowerScript : ScriptableObject
{
   public string towerName;
   public int cost;
   public GameObject tower;
   public Image icon;
}
