using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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

    [Header("Volume mixers")]
    public AudioMixer masterMixer;
    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup fXMixerGroup;
    public AudioMixerGroup musicMixerGroup;

    [Header("Volume Sliders")]
    public Slider masterSlider;
    public Slider fXSlider;
    public Slider musicSlider;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        fullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = fullscreen;
        resolution = Screen.currentResolution;
        resolutionText.text = resolution.ToString();
        resolutions = Screen.resolutions;
        audioSource = GetComponent<AudioSource>();
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

    private float intToDb(float level)
    {
        return level * 10 - 80;
    }

    public void IncreaseMasterVol()
    {
        if(masterSlider.value < masterSlider.maxValue)
        {
            masterMixer.SetFloat("MasterVol", intToDb(masterSlider.value + 1));
            masterSlider.value += 1;
            audioSource.outputAudioMixerGroup = masterMixerGroup;
            audioSource.Play();
        }
    }

    public void DecreaseMasterVol()
    {
        if (masterSlider.value > masterSlider.minValue)
        {
            masterMixer.SetFloat("MasterVol", intToDb(masterSlider.value - 1));
            masterSlider.value -= 1;
            audioSource.outputAudioMixerGroup = masterMixerGroup;
            audioSource.Play();
        }
    }    
    
    public void IncreaseFXVol()
    {
        if (fXSlider.value < fXSlider.maxValue)
        {
            masterMixer.SetFloat("FXVol", intToDb(fXSlider.value + 1));
            fXSlider.value += 1;
            audioSource.outputAudioMixerGroup = fXMixerGroup;
            audioSource.Play();
        }
    }

    public void DecreaseFXVol()
    {
        if (fXSlider.value > fXSlider.minValue)
        {
            masterMixer.SetFloat("FXVol", intToDb(fXSlider.value - 1));
            fXSlider.value -= 1;
            audioSource.outputAudioMixerGroup = fXMixerGroup;
            audioSource.Play();
        }
    }

    public void IncreaseMusicVol()
    {
        if (musicSlider.value < musicSlider.maxValue)
        {
            masterMixer.SetFloat("MusicVol", intToDb(musicSlider.value + 1));
            musicSlider.value += 1;
            audioSource.outputAudioMixerGroup = musicMixerGroup;
            audioSource.Play();
        }
    }

    public void DecreaseMusicVol()
    {
        if (musicSlider.value > musicSlider.minValue)
        {
            masterMixer.SetFloat("MusicVol", intToDb(musicSlider.value - 1));
            musicSlider.value -= 1;
            audioSource.outputAudioMixerGroup = musicMixerGroup;
            audioSource.Play();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
