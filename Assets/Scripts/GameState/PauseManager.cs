using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Singleton { get; private set; }

    [SerializeField]
    public GameObject ui;

    // An array of all audiosources in the game.
    public AudioSource[] audioSources;

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
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : initialTimeScale;     
        ui.SetActive(IsPaused);

        // Pause every audiosource in array.
        foreach(AudioSource a in audioSources)
        {
            if (IsPaused)
                a.Pause();
            else
                a.UnPause();
        }
        // Reduce the listener volume level
        AudioListener.volume = IsPaused ? 0.3f : 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
