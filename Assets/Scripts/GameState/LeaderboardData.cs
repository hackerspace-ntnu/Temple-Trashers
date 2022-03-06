using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[Serializable]
public struct Highscore
{
    public int score;
    public string name;

    public Highscore(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
}

class HighscoreComparator : IComparer<Highscore>
{
    public int Compare(Highscore x, Highscore y)
    {
        if (x.score == 0)
            return y.score;
        if (y.score == 0)
            return x.score;

        return y.score.CompareTo(x.score);
    }
}

public static class LeaderboardData
{
    // Path to where highscore data is saved
    private static readonly string highscoresDataPath = $"{Application.persistentDataPath}/highscores.data";

    /// <summary>
    /// Add a new score to the leaderboard
    /// </summary>
    public static void AddScore(int score, string name)
    {
        if (score == 0)
            return;
        List<Highscore> highscores = LoadScores();
        highscores.Add(new Highscore(score, name));

        // Sort the struct using a custom comparator
        highscores.Sort(new HighscoreComparator());

        SaveScores(highscores);
    }

    /// <summary>
    /// Save all the given scores
    /// </summary>
    private static void SaveScores(List<Highscore> scores)
    {
        using (FileStream stream = File.OpenWrite(highscoresDataPath))
        {
            new BinaryFormatter().Serialize(stream, scores);
        }
    }

    /// <summary>
    /// Load all saved scores
    /// </summary>
    /// <returns>An ordered list of <c>Highscore</c> structs.</returns>
    public static List<Highscore> LoadScores()
    {
        if (!File.Exists(highscoresDataPath))
            return CreateMockLeaderboard();

        List<Highscore> data;
        try
        {
            using (FileStream stream = File.OpenRead(highscoresDataPath))
            {
                data = (List<Highscore>)new BinaryFormatter().Deserialize(stream);
            }
        } catch (SystemException e) when (e is SerializationException || e is InvalidCastException)
        {
            File.Delete(highscoresDataPath);
            Debug.LogWarning($"Highscore data was corrupted, it has been replaced.\nException message: {e.Message}");
            return LoadScores();
        }

        // check scores for 0 values
        foreach (Highscore highScore in data)
        {
            if (highScore.score == 0)
            {
                File.Delete(highscoresDataPath);
                return LoadScores();
            }
        }

        return data;
    }

    private static List<Highscore> CreateMockLeaderboard()
    {
        // No leaderboard exists, so create a mock leaderboard
        List<Highscore> highscores = new List<Highscore>
        {
            new Highscore(5000, "The Archetype"),
            new Highscore(4500, "Fuereoduriko"),
            new Highscore(4000, "Dabble"),
            new Highscore(3500, "Frisk"),
            new Highscore(3000, "Jesper"),
            new Highscore(2500, "Rodrigues"),
            new Highscore(2000, "Zedd"),
            new Highscore(1500, "Grønnmerke"),
            new Highscore(1000, "KHTangent"),
            new Highscore(10, "Endie"),
        };

        SaveScores(highscores);
        return highscores;
    }
}
