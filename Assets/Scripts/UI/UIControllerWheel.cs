using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TowerScript[] towers;
    public GameObject[] menuSegments;
    private List<TowerScript> towersInMenu = new List<TowerScript>();
    public List<SpriteRenderer> iconHolders;
    public Sprite highlightSprite;
    public Sprite normalSprite;
    private GameObject selected;


    void Start()
    {
        if (towers.Length == menuSegments.Length)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
                iconHolders[i].sprite = towers[i].icon;
            }
        }
    }
    //Gets the tower gameobject stored in the corresponding scriptableobject that the uisegments uses.
    public TowerScript GetTower(int index)
    {
       for (int i = 0; i < menuSegments.Length; i++)
        {
            towersInMenu.Add(towers[i]);
        }
        return towersInMenu[index];
    }
    //Sets a single UI element to the highlight texture.
    public void HighlightSegment(int index)
    {
        LeanTween.scale(menuSegments[index], new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseLinear();
        iconHolders[index].sprite = towers[index].iconHighlight;
        menuSegments[index].GetComponent<SpriteRenderer>().sprite = highlightSprite;
        selected = menuSegments[index];
    }

    //Sets all UI elements back to their non-highlighted textures.
    public void NormalizeSegments()
        
    {
        for (int i = 0; i < menuSegments.Length; i++)
        {
            if (menuSegments[i] != selected) { 
            LeanTween.scale(menuSegments[i], new Vector3(1, 1, 1), 0.2f).setEaseLinear();
            iconHolders[i].sprite = towers[i].icon;
            menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
        }

    }
}
