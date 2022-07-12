using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] entries = default;

    [SerializeField]
    private Transform leaderboardBody = default;

    void Start()
    {
        List<Highscore> highscores = LeaderboardData.LoadScores();

        entries = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            Transform entry = leaderboardBody.GetChild(i);
            entry.GetChild(0).GetComponent<TextMeshProUGUI>().text = highscores[i].name;
            entry.GetChild(1).GetComponent<TextMeshProUGUI>().text = highscores[i].score.ToString();
        }
    }
}
