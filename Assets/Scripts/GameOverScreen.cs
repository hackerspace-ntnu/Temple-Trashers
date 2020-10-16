using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Game Over Screen Instantiated");
    }

    public void Restart()
    {
        Debug.Log("RESTARTED");
        // Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
