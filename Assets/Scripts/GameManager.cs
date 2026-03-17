using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Make it a singleton
    public int Score = 0; //The player's score
    public int LivePoints = 3; //The player's lives
    public int ElapsedTime = 0; //The elapsed time in seconds

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
        ElapsedTime = 0;
        SceneManager.LoadScene("Scenes/MainMenuScene"); // reset the game
    }

    private void Update()
    {
        ElapsedTime += (int)Time.deltaTime;
    }
}
