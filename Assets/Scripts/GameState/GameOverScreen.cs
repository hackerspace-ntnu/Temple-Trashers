using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Contains button functionality for the Game Over screen UI Canvas
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    public Text nameInput;
    public Text errorMsg;
    public Text scoreText;

    private PauseManager pm;

    private void Start()
    {
        scoreText.text = UIManager.Singleton.score.ToString();
        pm = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        pm.gameObject.SetActive(false);
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
            SceneManager.LoadScene("Endless Mode");
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
            SceneManager.LoadScene("Main Menu");
        }
    }
}
