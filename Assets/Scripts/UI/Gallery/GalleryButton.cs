using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryButton : MonoBehaviour
{
    private static float positionOffset = default;

    [SerializeField]
    private float offsetIncrement = default;

    private static GameObject galleryView = default;

    private GalleryEntry scriptableObject = default;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    void Start()
    {
        GetComponent<RectTransform>().LeanMoveLocalY(positionOffset, 0.2f);
        positionOffset += offsetIncrement;
        var color = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        textMesh.text = scriptableObject.name;
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
        //return SteamManager.Singleton.IsAchievementUnlocked(scriptableObject.achievementId);
        return true;
    }

    public void SelectButton()
    {
        if (!IsUnlocked())
        {
            //TODO: Set showcaseObject to "locked"
            return;
        }
        galleryView.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = scriptableObject.description;
        //galleryView.transform.Find("ObjectView");

        //TODO: Update galleryView with data from scriptableObject
    }

}
