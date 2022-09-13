using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GalleryEntry")]
public class GalleryEntry : ScriptableObject
{
    //Prefab used as showcase model
    public GameObject prefab = default;

    //Text to go with the model
    public string description = default;

    //The achievementId to unlock this entry (achievementId's are defined or changed in steamworks)
    public string achievementId = default;

}
