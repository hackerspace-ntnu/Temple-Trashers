using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIWheel : MonoBehaviour
{
    [SerializeField]
    private TowerScriptableObject[] towers = default;

    [SerializeField]
    private Transform iconOffset = default;

    [SerializeField]
    private Transform middleOrigin = default;

    [SerializeField]
    private GameObject iconWrapper = default;

    private List<GameObject> iconWrappers = new List<GameObject>();

    private float iconDegree = default;

    private int? _selectedSegmentIndex = null;

    [SerializeField]
    private float highlightedSpriteScale = 1.8f;

    [SerializeField]
    private float highlightScaleAnimationDuration = 0.2f;

    public int? SelectedSegmentIndex
    {
        get => _selectedSegmentIndex;
        set
        {
            if (value == _selectedSegmentIndex)
                return;

            _selectedSegmentIndex = value;
            if (_selectedSegmentIndex != null)
            {
                int index = (int)_selectedSegmentIndex;
                LeanTween.scale(iconWrappers[index], highlightedSpriteScale * Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
            }

            NormalizeSegments();
        }
    }

    void Awake()
    {
        //Calculate what share of degress from the wheel each icon should have
        iconDegree = 360f / towers.Length;

        //Create and store icon objects. 
        for (int i = 0; i < towers.Length; i++)
        {
            GameObject icon = Instantiate(iconWrapper, iconOffset.position, transform.rotation, transform);
            icon.GetComponent<SpriteRenderer>().sprite = towers[i].icon;
            icon.transform.RotateAround(middleOrigin.position, Vector3.forward, -i * iconDegree);
            iconWrappers.Add(icon);
        }

        gameObject.SetActive(false);
    }


    void OnEnable()
    {
        SelectedSegmentIndex = 0;

        // Reset the scale of all menu segments
        foreach (GameObject icon in iconWrappers)
            LeanTween.scale(icon, Vector3.one, 0f);
    }

    public TowerScriptableObject GetSelectedTower()
    {
        return towers[(int)SelectedSegmentIndex];
    }

    public TowerScriptableObject GetTower(int index)
    {
        return towers[index];
    }

    public int GetNumSegments()
    {
        return towers.Length;
    }

    private void NormalizeSegments()
    {
        for (int i = 0; i < iconWrappers.Count; i++)
        {
            if (i == SelectedSegmentIndex)
                continue;

            LeanTween.scale(iconWrappers[i], Vector3.one, highlightScaleAnimationDuration).setEaseLinear();
        }
    }
}
