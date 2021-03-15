using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TowerScript[] towers;
    public GameObject[] menuSegments;
    private List<GameObject> towersInMenu = new List<GameObject>();
    public List<SpriteRenderer> iconHolders;
    public Sprite highlightSprite;
    public Sprite normalSprite;


    void Start()
    {
        if (towers.Length == menuSegments.Length)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                iconHolders[i].sprite = towers[i].icon;
                // menuSegments[i].GetComponentInChildren<SpriteRenderer>().sprite = towers[i].icon;
                //menuSegments[i].setIcon(towers[i].icon);
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
    public void highlightSegment(int index)
    {
        LeanTween.scale(menuSegments[index], new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseInOutBounce();
        menuSegments[index].GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }
    public void normalizeSegments()
        
    {
        for (int i = 0; i < menuSegments.Length; i++)
        {
            LeanTween.scale(menuSegments[i], new Vector3(1, 1, 1), 0.2f).setEaseLinear();
            menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
            
    }
}
