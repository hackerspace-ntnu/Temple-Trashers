using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField]
    Resolution[] resolutions;   // Possible resolutions

    [SerializeField]
    Resolution resolution;  // Current resolution

    public TMPro.TextMeshProUGUI resolutionText; // TMP element that displays resolution

    [SerializeField]
    private bool fullscreen;    // Boolean flag for fullscreen

    public Toggle fullscreenToggle; // Toggle UI element for fullscreen



    // Start is called before the first frame update
    void Start()
    {
        fullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = fullscreen;
        resolution = Screen.currentResolution;
        resolutionText.text = resolution.ToString();
        resolutions = Screen.resolutions;
    }

    public void NextResolution(){
        // Find current resolution in list
        int index = Array.IndexOf(resolutions, resolution);

        // Select the next resolution
        if (index == resolutions.Length - 1)
            index = -1;

        resolution = resolutions[index + 1];

        resolutionText.text = resolution.ToString();
    }

    public void PrevResolution()
    {
        // Find current resolution in list
        int index = Array.IndexOf(resolutions, resolution);

        // Select the next resolution
        if (index == 0)
            index = resolutions.Length;

        resolution = resolutions[index - 1];

        resolutionText.text = resolution.ToString();
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }

    public void Fullscreen()
    {
        fullscreen = !fullscreen;
        fullscreenToggle.isOn = fullscreen;
        ApplyResolution();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
