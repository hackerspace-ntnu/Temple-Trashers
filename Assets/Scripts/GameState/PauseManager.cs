using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Singleton { get; private set; }

    [SerializeField]
    public GameObject ui;

    public bool IsPaused { get; private set; } = false;

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

    public void PauseGame()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
        ui.SetActive(IsPaused);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
