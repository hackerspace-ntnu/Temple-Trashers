using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct Difficulty
{
    public string level;
    public float spawnRate;
    public float waveInterval;
}

public class MainMenu : MonoBehaviour
{
    public Difficulty[] difficulties;
    private Difficulty selectedDifficulty;
    public TextMeshProUGUI difficultyText;

    private void Start()
    {
        selectedDifficulty = difficulties[0];
        SaveDifficulty();
    }
    // Simple function called to load the next scene from the built in unity button
    public void StartGame()
    {
        SceneManager.LoadScene("Endless_Mode");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Gallery()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void ChangeDifficulty()
    {
        int index = Array.IndexOf(difficulties, selectedDifficulty);

        if (index == difficulties.Length - 1)
        {
            selectedDifficulty = difficulties[0];
        }
        else
            selectedDifficulty = difficulties[index + 1];
        difficultyText.text = "Difficulty: " + selectedDifficulty.level;

        SaveDifficulty();
    }

    private void SaveDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", selectedDifficulty.level);
        PlayerPrefs.SetFloat("spawnRate", selectedDifficulty.spawnRate);
        PlayerPrefs.SetFloat("waveInterval", selectedDifficulty.waveInterval);
    }
}
