using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public struct Highscore {
    public int score;
    public string name;

    public Highscore(int Score, string Name)
    {
        score = Score;
        name = Name;
    }
}

class HighscoreComparator : IComparer<Highscore>
{
    public int Compare(Highscore x, Highscore y)
    {
        if (x.score == 0)
        {
            return y.score;
        }
        if(y.score == 0)
        {
            return x.score;
        }

        return y.score.CompareTo(x.score);
    }

}

public static class LeaderboardData
{
    // Path to where highscore data is saved
    private static string path = Application.persistentDataPath + "/highscores.data";

    /// <summary>
    /// Add a new score to the leaderboard
    /// </summary>
    /// <param name="score"></param>
    /// <param name="name"></param>
    public static void AddScore(int score, string name)
    {
        List<Highscore> highscores = LoadScores();
        highscores.Add(new Highscore(score, name));

        // Sort the struct using a custom comparator
        HighscoreComparator HC = new HighscoreComparator();

        highscores.Sort(HC);

        foreach(Highscore h in highscores)
        {
            Debug.Log(h.name + " : " + h.score.ToString());
        }
        SaveScores(highscores);
    }

    /// <summary>
    /// Save all the given scores
    /// </summary>
    /// <param name="scores"></param>
    public static void SaveScores(List<Highscore> scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, scores);

        stream.Close();
    }

    /// <summary>
    /// Load all saved scores
    /// </summary>
    /// <returns>Returns a ordered list of Highscore structs</returns>
    public static List<Highscore> LoadScores()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<Highscore> data;

            // Check if the file is corrupted
            try
            {
                data = formatter.Deserialize(stream) as List<Highscore>;
            }
            catch
            {
                stream.Close();
                File.Delete(path);
                Debug.LogWarning("Highscore data was corrupted, it has been replaced.");
                return LoadScores();
            }

            stream.Close();

            return data;
        }
        else
        {
            List<Highscore> highscores = new List<Highscore>();
            
            // No leaderboard exists create a default one
            highscores.Add(new Highscore(5000, "The Archetype"));
            highscores.Add(new Highscore(4500, "Fuereoduriko"));
            highscores.Add(new Highscore(4000, "Dabble"));
            highscores.Add(new Highscore(3500, "Frisk"));
            highscores.Add(new Highscore(3000, "Jesper"));
            highscores.Add(new Highscore(2500, "Rodrigues"));
            highscores.Add(new Highscore(2000, "Zedd"));
            highscores.Add(new Highscore(1500, "Grønnmerke"));
            highscores.Add(new Highscore(1000, "KHTangent"));
            highscores.Add(new Highscore(10, "Endie"));

            SaveScores(highscores);
            return highscores;
        }
    }
}
