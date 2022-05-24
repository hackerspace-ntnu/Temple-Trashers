using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWheel : MonoBehaviour
{

    [SerializeField]
    private TowerScriptableObject[] towers = default;
    private List<Sprite> icons;

    void Start()
    {
        foreach (TowerScriptableObject tower in towers)
        {
            icons.Add(tower.icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
