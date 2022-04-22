using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIControllerWheel : MonoBehaviour
{
    [SerializeField]
    private TowerScriptableObject[] towers = default;

    [SerializeField]
    private GameObject[] menuSegments = default;

    [SerializeField]
    private List<SpriteRenderer> iconHolders = default;

    [SerializeField]
    private Sprite normalSprite = default;

    [SerializeField]
    private Sprite highlightSprite = default;

    [SerializeField]
    private float highlightedSpriteScale = 1.2f;

    [SerializeField]
    private float highlightScaleAnimationDuration = 0.2f;

    private int? _selectedSegmentIndex = null;

    public int? SelectedSegmentIndex
    {
        get => _selectedSegmentIndex;
        set
        {
            if (value < 0 || value >= menuSegments.Length)
                throw new IndexOutOfRangeException();

            if (value == _selectedSegmentIndex)
                return;

            _selectedSegmentIndex = value;
            if (_selectedSegmentIndex != null)
            {
                int index = (int)_selectedSegmentIndex;
                LeanTween.scale(menuSegments[index], highlightedSpriteScale * Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
                iconHolders[index].sprite = towers[index].iconHighlight;
                menuSegments[index].GetComponent<SpriteRenderer>().sprite = highlightSprite;
            }

            NormalizeSegments();
        }
    }

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

    void OnEnable()
    {
        SelectedSegmentIndex = 0;

        // Reset the scale of all menu segments
        foreach (GameObject segment in menuSegments)
            LeanTween.scale(segment, Vector3.one, 0f);
    }

    public int GetNumSegments()
    {
        return menuSegments.Length;
    }

    /// <summary>
    /// Returns the <c>TowerScriptableObject</c> at the provided index of the wheel segments.
    /// </summary>
    public TowerScriptableObject GetTower(int index)
    {
        return towers[index];
    }

    public TowerScriptableObject GetSelectedTower()
    {
        if (SelectedSegmentIndex == null)
            return null;

        return GetTower((int)SelectedSegmentIndex);
    }

    /// <summary>
    /// Sets all non-selected segments back to their non-highlighted textures.
    /// </summary>
    public void NormalizeSegments()
    {
        for (int i = 0; i < menuSegments.Length; i++)
        {
            if (i == SelectedSegmentIndex)
                continue;

            LeanTween.scale(menuSegments[i], Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
            iconHolders[i].sprite = towers[i].icon;
            menuSegments[i].GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
    }
}
