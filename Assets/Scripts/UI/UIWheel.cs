using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWheel : MonoBehaviour
{

    [SerializeField]
    private TowerScriptableObject[] towers = default;
    [SerializeField]
    private Transform iconOffset;
    [SerializeField]
    private Transform middleOrigin;
    [SerializeField]
    private GameObject iconWrapper;
    private List<Sprite> icons = new List<Sprite>();
    private float iconDegree = default;

    void Start()
    {
        //Add all available icon sprites to list
        foreach (TowerScriptableObject tower in towers)
        {
            icons.Add(tower.icon);
        }
        //Calculate what share of degress from the wheel each icon should have
        iconDegree = 360/icons.Count;

        for (int i = 0; i < icons.Count; i++)
        {
            GameObject icon = Instantiate(iconWrapper, iconOffset.position, transform.rotation, transform);
            icon.GetComponent<SpriteRenderer>().sprite = icons[i];
            icon.transform.RotateAround(middleOrigin.position, Vector3.forward, i*iconDegree);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
