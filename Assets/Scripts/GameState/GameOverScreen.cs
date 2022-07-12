﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


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
            errorMsg.enabled = true;
            return;
        }

        // Update leaderboard
        LeaderboardData.AddScore(UIManager.Singleton.Score, nameInput.text);

        SceneManager.LoadScene(sceneName);
    }
}
