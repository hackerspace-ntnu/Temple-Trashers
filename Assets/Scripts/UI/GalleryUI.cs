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
        foreach (GalleryEntry entry in entries)
        {
            var newButton = Instantiate(button, buttonHolder.transform);
            //TODO: refactor to be done in button script
            newButton.GetComponent<TextMesh>().text = entry.name;
            //TODO: set button reference to entry

        }
    }

}
