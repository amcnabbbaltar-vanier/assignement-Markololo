using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
   
    private bool isPaused = false;

    void Update()
    {
        // Check if the player presses the "Escape" key (or any key you choose).
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // This method pauses the game.
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        // Freeze game time
        Time.timeScale = 0f;
        isPaused = true;
    }

    // This method resumes the game.
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        // Application.Quit();
        // For a Main Menu scene:
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
    public void RestartGame()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}

