using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text timerText = null;

    void Start()
    {
        StartCoroutine(Countdown(5));
    }



    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            timerText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            // Fix UI
            counter--;
        }
        Restart();
    }


    public void Restart()
    {   
        // Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
