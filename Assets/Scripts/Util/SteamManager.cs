using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public static SteamManager Singleton { get; private set; }
    private string playerName = SteamClient.Name;
    private float playerSteamId = SteamClient.SteamId;

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

        DontDestroyOnLoad(this);
        try
        {
            //Tell steam that player is playing Temple Trashers
            Steamworks.SteamClient.Init(1984140, true);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    void Update()
    {
        //Used for syncronizing steam with the game (Must be called every update according to documentation)
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }

    public string getPlayerName()
    {
        return playerName;
    }
}
