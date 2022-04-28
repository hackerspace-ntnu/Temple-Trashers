using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseManager : MonoBehaviour
{
    public static PauseManager Singleton { get; private set; }

    [SerializeField]
    private GameObject ui = default;

    // An array of all audiosources in the game.
    [SerializeField]
    private AudioSource[] audioSources = default;

    [SerializeField]
    private float normalAudioVolume = 1f;

    [SerializeField]
    private float pauseAudioVolume = 0.3f;

    public bool IsPaused { get; private set; } = false;

    private float initialTimeScale;

    void Awake()
    {
        #region Singleton boilerplate

        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Debug.LogWarning($"There's more than one {Singleton.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        Singleton = this;

        #endregion Singleton boilerplate

        ui.SetActive(IsPaused);
    }

    void Start()
    {
        initialTimeScale = Time.timeScale;
    }

    public void PauseGame()
    {
        if (!gameObject.activeSelf)
            return;

        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : initialTimeScale;
        ui.SetActive(IsPaused);

        // Pause every audiosource in array.
        foreach (AudioSource source in audioSources)
        {
            if (IsPaused)
                source.Pause();
            else
                source.UnPause();
        }

        // Reduce the listener volume level
        AudioListener.volume = IsPaused ? pauseAudioVolume : normalAudioVolume;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
