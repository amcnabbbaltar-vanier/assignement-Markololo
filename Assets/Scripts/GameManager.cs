using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Make it a singleton
    public int Score = 0; //The player's score

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
        Score = 0; //Reset the score to 0
    }
}
