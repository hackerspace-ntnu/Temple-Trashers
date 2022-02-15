using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private Text[] names;

    [SerializeField]
    private Text[] scores;

    public Transform leaderboardBody;

    private void Start()
    {
        names = new Text[10];
        scores = new Text[10];

        Highscores highscores = LeaderboardData.LoadScores();

        for(int i = 0; i < 10; i++)
        {
            names[i] = leaderboardBody.GetChild(i).GetChild(0).GetComponent<Text>();
            scores[i] = leaderboardBody.GetChild(i).GetChild(1).GetComponent<Text>();

            names[i].text = highscores.name[i];
            scores[i].text = highscores.score[i].ToString();
        }
    }
}
