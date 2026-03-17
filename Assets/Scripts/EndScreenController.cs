using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    private Text scoreText;
    private Text livePointsText;
    void Start()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        livePointsText = GameObject.FindWithTag("Points").GetComponent<Text>();
        scoreText.text = "Final Score: " + GameManager.Instance.Score.ToString();
        livePointsText.text = "Live Points: " + GameManager.Instance.LivePoints.ToString();
    }
}
