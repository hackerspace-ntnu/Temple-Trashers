using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TowerScript[] towers;
    public FlexibleUIButton[] menuSegments;
    private List<GameObject> towersInMenu = new List<GameObject>();


    void Start()
    {
        if (towers.Length == 4 && menuSegments.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                menuSegments[i].setIcon(towers[i].icon);
            }
        }
    }
    public GameObject getTower(int index)
    {
       Debug.Log(index);
       for (int i = 0; i < menuSegments.Length; i++)
        {
            towersInMenu.Add(towers[i].tower);
        }
        return towersInMenu[index];
    }

}
