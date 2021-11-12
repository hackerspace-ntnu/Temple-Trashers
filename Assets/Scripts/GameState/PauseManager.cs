using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private static GameObject ui;

    public static PauseManager Singleton { get; private set; }
    

    private static bool pauseStatus = false;
   
    public static bool isPaused()
    {
        return pauseStatus;
    }

    public static void pauseGame()
    {
        if (pauseStatus == false)
        {
            pauseStatus = true;
            Time.timeScale = 0;
            ui.SetActive(pauseStatus);
        }
        else
        {
            pauseStatus = false;
            Time.timeScale = 1;
            ui.SetActive(pauseStatus);
        }
    }

    public static void quitGame()
    {
        Application.Quit();
    }

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

        ui = GameObject.Find("PauseMenu");
        if (ui == null)
        {
            Debug.Log("missing Pause UI in scene.");
        }
        else
        {
            ui.SetActive(pauseStatus);
        }
    }
}
