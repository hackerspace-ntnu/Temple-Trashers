using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Highscores {
    public int[] score;
    public string[] name;

    public Highscores(int[] Score, string[] Name)
    {
        score = Score;
        name = Name;
    }
}

public static class LeaderboardData
{
    // Path to where highscore data is saved
    private static string path = Application.persistentDataPath + "/highscores.data";

    public static void AddScore(int score, string name)
    {
        Highscores highscores = LoadScores();
        Highscores newScores = highscores;

        for(int i = 0; i < 10; i++)
        {
            if(highscores.score[i] < score)
            {
                newScores.score[i] = score;
                newScores.name[i] = name;

                for(int n = i + 1; n < 10; n++)
                {
                    newScores.score[n] = highscores.score[n - 1];
                    newScores.name[n] = highscores.name[n - 1];
                }

                break;
            }
        }

        SaveScores(newScores);
    }

    public static void SaveScores(Highscores scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, scores);

        stream.Close();
    }

    public static Highscores LoadScores()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Highscores data = formatter.Deserialize(stream) as Highscores;
            stream.Close();

            if(data.name.Length < 10 || data.score.Length < 10)
            {
                // Scores are missing, delete the data and reset
                File.Delete(path);
                data = LoadScores();
            }

            return data;
        }
        else
        {
            // No leaderboard exists create a default one
            int[] defaultScores = new int[10]{
            5000,
            4500,
            4000,
            3500,
            3000,
            2500,
            2000,
            1500,
            1000,
            0};

            string[] defaultNames = new string[10]
            {
            "The Archetype",
            "Fuereoduriko",
            "Dabble",
            "Frisk",
            "Jesper",
            "Rodrigues",
            "Zedd",
            "Grønnmerke",
            "KHTangent",
            "Endie"
            };

            Highscores data = new Highscores(defaultScores, defaultNames);

            SaveScores(data);
            return data;
        }
    }
}
