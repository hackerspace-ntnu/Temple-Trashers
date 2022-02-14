using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text timerText;

    public float time = 5f;

    void Update()
    {
        timerText.text = Mathf.Round(time).ToString();
        // Fix UI
        time -= Time.deltaTime;

        if (time <= 0)
            Restart();
    }

    public void Restart()
    {
        // Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
