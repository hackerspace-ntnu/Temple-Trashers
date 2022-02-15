using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text nameInput;
    public Text errorMsg;
    public Text scoreText;

    private void Start()
    {
        scoreText.text = UIManager.Singleton.score.ToString();
    }

    public void Restart()
    {
        if(nameInput.text == "")
        {
            errorMsg.enabled = true;
        }
        else
        {
            // Update Leaderboard 
            LeaderboardData.AddScore(UIManager.Singleton.score, nameInput.text);

            // Loads Main menu
            SceneManager.LoadScene(2);
        }
    }

    public void MainMenu()
    {
        if (nameInput.text == "")
        {
            errorMsg.enabled = true;
        }
        else
        {
            // Update Leaderboard 
            LeaderboardData.AddScore(UIManager.Singleton.score, nameInput.text);

            // Loads Main menu
            SceneManager.LoadScene(1);
        }
    }
}
