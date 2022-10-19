using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private Transform leaderboardBody = default;

    void Start()
    {
        List<Highscore> highscores = LeaderboardData.LoadScores();

        for (int i = 0; i < 10; i++)
        {
            Transform entry = leaderboardBody.GetChild(i);
            entry.GetChild(0).GetComponent<TextMeshProUGUI>().text = highscores[i].name;
            entry.GetChild(1).GetComponent<TextMeshProUGUI>().text = highscores[i].score.ToString();
        }

        List<Transform> leaderboardTiles = new List<Transform>();
        for (int i = 0; i < 10; i++)
        {
            leaderboardTiles.Add(leaderboardBody.GetChild(i));
        }
        Task.Run(() => SteamManager.Singleton.GetLeaderboard(leaderboardTiles));
    }
}
