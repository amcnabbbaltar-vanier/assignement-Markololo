using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Make it a singleton
    public int Score = 0; //The player's score
    public int LivePoints = 3; //The player's lives

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Destroy duplicates if it happens
        }
    }
    public void ResetGame()
    {
        Score = 0; 
        LivePoints = 3; 
        SceneManager.LoadScene("Scenes/MainMenuScene"); // reset the game
    }
}
