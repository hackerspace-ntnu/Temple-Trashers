using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Contains button functionality for the Game Over screen UI Canvas
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text nameInput;

    [SerializeField]
    private Text errorMsg;

    [SerializeField]
    private Text scoreText;

    void Start()
    {
        scoreText.text = UIManager.Singleton.score.ToString();
        PauseManager.Singleton.gameObject.SetActive(false);
    }

    public void Restart()
    {
        UpdateLeaderboardAndLoadScene("Endless Mode");
    }

    public void MainMenu()
    {
        UpdateLeaderboardAndLoadScene("Main Menu");
    }

    private void UpdateLeaderboardAndLoadScene(string sceneName)
    {
        if (nameInput.text == "")
        {
            errorMsg.enabled = true;
            return;
        }

        // Update leaderboard
        LeaderboardData.AddScore(UIManager.Singleton.score, nameInput.text);

        SceneManager.LoadScene(sceneName);
    }
}
