using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
