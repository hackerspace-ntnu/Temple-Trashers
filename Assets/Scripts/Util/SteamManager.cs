using Steamworks;
using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public static SteamManager Singleton { get; private set; }
    private string playerName = "";
    private float playerSteamId = 0;
    //Round based achievement tracking
    private int golemsDestroyed = 0;
    //Global leaderboard

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
            playerName = SteamClient.Name;
            playerSteamId = SteamClient.SteamId;
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

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetAchievement(string name)
    {
        if (!SteamClient.IsValid){return;}

        switch (name){
            case "ACH_SLAPPED_GOLEM":
                golemsDestroyed++;
                if (golemsDestroyed == 5)
                {
                    var ach1 = new Achievement(name);
                    ach1.Trigger();
                }
                break;
            default:
                var ach = new Achievement(name);
                ach.Trigger();
                break;

        }
        
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        var ach = new Achievement(achievementId);
        if (ach.State) { return true; }
        return false;
    }

    public void ResetAchievementProgress()
    {
        golemsDestroyed = 0;
    }

    public async Task GetLeaderboard(List<Transform> tileParent)
    {
        var leaderboard = await SteamUserStats.FindLeaderboardAsync("Scoreboard");
        if (leaderboard.HasValue)
        {
            var globalScores = await leaderboard.Value.GetScoresAsync(tileParent.Count);

            for (int i = 0; i < tileParent.Count; i++)
            {
                tileParent[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = globalScores[i].User.Name;
                tileParent[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = globalScores[i].Score.ToString();
            }
        }
    }

    public async Task AddScore(int score)
    {
        var leaderboardGlobal = await SteamUserStats.FindLeaderboardAsync("Scoreboard");
        if (leaderboardGlobal.HasValue)
        {
            await leaderboardGlobal.Value.SubmitScoreAsync(score);
        }
            
    }
    
}
