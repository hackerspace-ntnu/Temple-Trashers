using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text timerText = null;

    public float time = 5f;

    private void Update()
    {
        timerText.text = Mathf.Round(time).ToString();
        // Fix UI
        time = time - Time.deltaTime;

        if(time <= 0)
        {
            Restart();
        }
    }


    public void Restart()
    {   
        // Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
