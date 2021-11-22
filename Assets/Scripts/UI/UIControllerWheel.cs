using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerWheel : MonoBehaviour
{
    [SerializeField]
    private TowerScript[] towers;

    [SerializeField]
    private GameObject[] menuSegments;

    [SerializeField]
    private List<SpriteRenderer> iconHolders;

    public Sprite normalSprite;
    public Sprite highlightSprite;
    public float highlightedSpriteScale = 1.2f;
    public float highlightScaleAnimationDuration = 0.2f;

    private GameObject selected;

    void Awake()
    {
        if (menuSegments.Length != towers.Length || iconHolders.Count != towers.Length)
        {
            throw new ArgumentException(
                $"The sizes of `{nameof(towers)}`, `{nameof(menuSegments)}` and {nameof(iconHolders)} were not equal!"
            );
        }

        for (int i = 0; i < towers.Length; i++)
            iconHolders[i].sprite = towers[i].icon;

        foreach (GameObject segment in menuSegments)
            segment.GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    public int GetNumSegments()
    {
        return menuSegments.Length;
    }

    /// <summary>
    /// Returns the `TowerScript` (`ScriptableObject`) at the provided index of the wheel segments.
    /// </summary>
    public TowerScript GetTower(int index)
    {
        return towers[index];
    }

    /// <summary>
    /// Sets a single UI element to the highlight texture.
    /// </summary>
    public void HighlightSegment(int index)
    {
        LeanTween.scale(menuSegments[index], highlightedSpriteScale * Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
        iconHolders[index].sprite = towers[index].iconHighlight;
        menuSegments[index].GetComponent<SpriteRenderer>().sprite = highlightSprite;
        selected = menuSegments[index];
    }

    /// <summary>
    /// Sets all non-selected segments back to their non-highlighted textures.
    /// </summary>
    public void NormalizeSegments()
    {
        for (int i = 0; i < menuSegments.Length; i++)
        {
            if (menuSegments[i] == selected)
                continue;

            LeanTween.scale(menuSegments[i], Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
            iconHolders[i].sprite = towers[i].icon;
            menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
    }
}
