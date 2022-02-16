using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text nameInput;

    [SerializeField]
    private Text errorMsg;

    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        scoreText.text = UIManager.Singleton.score.ToString();
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
