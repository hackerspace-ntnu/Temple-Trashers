using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryUI : MonoBehaviour
{
    public GalleryEntry[] entries = default;

    public GameObject galleryView = default;

    public GameObject button = default;

    public GameObject buttonHolder = default;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            var newButton = Instantiate(button, buttonHolder.transform);
            var gb = newButton.GetComponent<GalleryButton>();
            gb.SetScriptableObject(entries[i]);
            gb.SetGalleryView(galleryView);
        }
    }

}
