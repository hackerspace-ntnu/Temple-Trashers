using System;
using System.Collections.Generic;
using UnityEngine;


// Code based on https://github.com/hackerspace-ntnu/Defendy-Pengy/blob/bcc46d1f6f4799df822622efbccf03fcee0b0f0b/Assets/Scripts/Util/WaveParser.cs
public class WaveFileParser
{
    private static List<EnemyWave> waves;
    private static EnemyWave currentWave;

    public static List<EnemyWave> ParseWaveFile(TextAsset waveFile)
    {
        waves = new List<EnemyWave>();
        currentWave = new EnemyWave();

        foreach (string l in waveFile.text.Split(new[] { "\n" }, StringSplitOptions.None))
        {
            string line = l.Trim().ToLower();
            if (line.Length == 0 || line.StartsWith("//"))
                continue;
            if (line.StartsWith("---"))
            {
                FinishWave();
                continue;
            }

            ParseLine(line);
        }

        FinishWave();

        return waves;
    }

    private static void FinishWave()
    {
        if (currentWave.IsEmpty())
            return;

        waves.Add(currentWave);
        currentWave = new EnemyWave();
    }

    private static void ParseLine(string line)
    {
        string[] tokens = line.Split(' ');
        try
        {
            ParseEnemy(tokens);
            return;
        } catch (FormatException)
        {}

        try
        {
            ParsePause(tokens);
            return;
        } catch (FormatException)
        {}

        throw new FormatException($"Was not able to parse the following line:\n{line}");
    }

    private static void ParseEnemy(string[] tokens)
    {
        int enemyCount = int.Parse(tokens[0]);
        Type enemy = GetEnemyType(tokens[1]);
        currentWave.AddEnemySubWave(enemy, enemyCount);
    }

    private static void ParsePause(string[] tokens)
    {
        if (tokens[0] != "pause")
            throw new FormatException();

        float pauseDuration = float.Parse(tokens[1]);
        currentWave.AddPauseSubWave(pauseDuration);
    }

    private static Type GetEnemyType(string token)
    {
        switch (token)
        {
            case "skeleton":
                return typeof(SkeletonController);

            default:
                throw new FormatException($"Invalid enemy type: {token}");
        }
    }
}
