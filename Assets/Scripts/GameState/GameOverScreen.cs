using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using System.Threading.Tasks;

/// <summary>
/// Contains button functionality for the Game Over screen UI Canvas
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameInput = default;

    [SerializeField]
    private TextMeshProUGUI errorMsg = default;

    [SerializeField]
    private TextMeshProUGUI scoreText = default;

    void Start()
    {
        scoreText.text = UIManager.Singleton.Score.ToString();
        PauseManager.Singleton.gameObject.SetActive(false);
        ControllerButtonNavigator.defaultButton = null;
        ControllerButtonNavigator.currentButton = null;
        //Achievement triggers
        if (Time.timeSinceLevelLoad < 30f) { SteamManager.Singleton.SetAchievement("ACH_SPEEDRUN"); }
        if (UIManager.Singleton.Score >= 2500) { SteamManager.Singleton.SetAchievement("ACH_SCORE_MIN"); }
        if (UIManager.Singleton.Score >= 5000) { SteamManager.Singleton.SetAchievement("ACH_SCORE_MID"); }
        if (UIManager.Singleton.Score >= 10000) { SteamManager.Singleton.SetAchievement("ACH_SCORE_MAX"); }
        SteamManager.Singleton.SetAchievement("ACH_TOUCH_GRASS");
        SteamManager.Singleton.ResetAchievementProgress();
    }

    public void Restart()
    {
        UpdateLeaderboardAndLoadScene("Endless_Mode");
    }

    public void MainMenu()
    {
        UpdateLeaderboardAndLoadScene("Main_Menu");
    }

    private void UpdateLeaderboardAndLoadScene(string sceneName)
    {
        if (nameInput.text == "")
        {
            if (!SteamClient.IsValid)
            {
                errorMsg.enabled = true;
                return;
            }
            Task.Run(() => SteamManager.Singleton.AddScore(UIManager.Singleton.Score));
        }

        UIInputController[] playerInputs = GameObject.FindObjectsOfType<UIInputController>();
        foreach(UIInputController controller in playerInputs)
        {
            Destroy(controller.gameObject);
        }

        // Update leaderboard
        LeaderboardData.AddScore(UIManager.Singleton.Score, SteamManager.Singleton.GetPlayerName());

        // Make sure slow-mo fx are normalized
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        SceneManager.LoadScene(sceneName);
    }
}
