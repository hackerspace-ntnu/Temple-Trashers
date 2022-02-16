using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Leaderboard : MonoBehaviour
{
    public GameObject highscoreEntry;

    [SerializeField]
    private GameObject[] entries;

    public Transform leaderboardBody;

    void Start()
    {
        Highscores highscores = LeaderboardData.LoadScores();

        entries = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            GameObject entry = Instantiate(highscoreEntry, leaderboardBody);
            entry.transform.GetChild(0).GetComponent<Text>().text = highscores.name[i];
            entry.transform.GetChild(1).GetComponent<Text>().text = highscores.score[i].ToString();
        }
    }
}
