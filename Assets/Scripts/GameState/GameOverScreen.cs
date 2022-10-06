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
        if (Time.timeSinceLevelLoad < 30f)
        {
            SteamManager.Singleton.SetAchievement("ACH_SPEEDRUN");
        }
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
        LeaderboardData.AddScore(UIManager.Singleton.Score, nameInput.text);

        SceneManager.LoadScene(sceneName);
    }
}
