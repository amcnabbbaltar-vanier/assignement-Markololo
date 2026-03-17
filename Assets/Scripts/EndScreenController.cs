using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    private Text scoreText;
    private Text livePointsText;
    private Text timerText;
    void Start()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        livePointsText = GameObject.FindWithTag("Points").GetComponent<Text>();
        scoreText.text = "Final Score: " + GameManager.Instance.Score.ToString();
        livePointsText.text = "Live Points: " + GameManager.Instance.LivePoints.ToString();
        timerText = GameObject.FindWithTag("Timer").GetComponent<Text>();
    }
}
