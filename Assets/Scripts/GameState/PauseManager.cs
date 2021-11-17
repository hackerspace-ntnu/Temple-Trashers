using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    public GameObject ui;

    public static PauseManager Singleton { get; private set; }
    

    private bool pauseStatus = false;
   
    public bool IsPaused()
    {
        return pauseStatus;
    }

    public void PauseGame()
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

    public void QuitGame()
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


            ui.SetActive(pauseStatus);
    }
}
