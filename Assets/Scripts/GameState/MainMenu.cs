﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Simple function called to load the next scene from the built in unity button
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}