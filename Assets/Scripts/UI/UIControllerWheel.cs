using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerWheel : MonoBehaviour
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

    /// <summary>
    /// Returns the tower `GameObject` stored in the corresponding `ScriptableObject` that the UI segments use.
    /// </summary>
    public TowerScript GetTower(int index)
    {
        towersInMenu.Clear();
        for (int i = 0; i < menuSegments.Length; i++)
            towersInMenu.Add(towers[i]);

        return towersInMenu[index];
    }

    /// <summary>
    /// Sets a single UI element to the highlight texture.
    /// </summary>
    public void HighlightSegment(int index)
    {
        LeanTween.scale(menuSegments[index], new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseLinear();
        iconHolders[index].sprite = towers[index].iconHighlight;
        menuSegments[index].GetComponent<SpriteRenderer>().sprite = highlightSprite;
        selected = menuSegments[index];
    }

    /// <summary>
    /// Sets all UI elements back to their non-highlighted textures.
    /// </summary>
    public void NormalizeSegments()
    {
        for (int i = 0; i < menuSegments.Length; i++)
        {
            if (menuSegments[i] != selected)
            {
                LeanTween.scale(menuSegments[i], new Vector3(1, 1, 1), 0.2f).setEaseLinear();
                iconHolders[i].sprite = towers[i].icon;
                menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
        }
    }
}
