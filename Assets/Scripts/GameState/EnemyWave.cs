using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A wave consisting of one or more sub-waves.
/// </summary>
public class EnemyWave
{
    /// <summary>
    /// One part of a wave, which can either be a number of enemies to spawn, or a duration to pause.
    /// </summary>
    public class SubWave
    {
        public readonly Type enemyType;

        public readonly int number;

        // -1 means that the wave is not a "pause wave"
        public readonly float pauseDuration = -1f;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public bool IsPause => pauseDuration != -1f;

        public SubWave(Type enemyType, int number)
        {
            this.enemyType = enemyType;
            this.number = number;
        }

        public SubWave(float pauseDuration)
        {
            this.pauseDuration = pauseDuration;
        }
    }

    private List<SubWave> subWaves = new List<SubWave>();

    public void AddEnemySubWave(Type enemyType, int number)
    {
        subWaves.Add(new SubWave(enemyType, number));
    }

    public void AddPauseSubWave(float pauseDuration)
    {
        subWaves.Add(new SubWave(pauseDuration));
    }

    public bool IsEmpty()
    {
        return subWaves.Count == 0;
    }

    public SubWave GetSubWave(int index)
    {
        if (index >= subWaves.Count)
            return null;
        return subWaves[index];
    }
}
