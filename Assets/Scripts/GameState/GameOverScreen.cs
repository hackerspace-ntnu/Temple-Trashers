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
            errorMsg.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(LoadScene(2));
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
            StartCoroutine(LoadScene(1));
        }
    }

    IEnumerator LoadScene(int sceneID)
    {
        // Update Leaderboard 
        LeaderboardData.AddScore(UIManager.Singleton.score, nameInput.text);

        // enter delay here to prevent us from crashing the game saving highscore data.
        yield return new WaitForSeconds(1f);

        // Loads Main menu
        SceneManager.LoadScene(sceneID);
    }
}
