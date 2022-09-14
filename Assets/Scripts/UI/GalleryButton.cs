using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryButton : MonoBehaviour
{
    private static float positionOffset = default;

    [SerializeField]
    private float offsetIncrement = default;

    private static GameObject galleryView = default;

    private GalleryEntry scriptableObject = default;

    void Start()
    {
        transform.Translate(new Vector3(0, positionOffset));
        positionOffset += offsetIncrement;
    }

    public void SetScriptableObject(GalleryEntry scObj)
    {
        scriptableObject = scObj;
    }

    public void SetGalleryView(GameObject obj)
    {
        galleryView = obj;
    }

    private bool IsUnlocked()
    {
        return SteamManager.Singleton.IsAchievementUnlocked(scriptableObject.achievementId);
    }

    public void SelectButton()
    {
        if (!IsUnlocked())
        {
            //TODO: Set showcaseObject to "locked"
            return;
        }

        //TODO: Update galleryView with data from scriptableObject
    }

}
