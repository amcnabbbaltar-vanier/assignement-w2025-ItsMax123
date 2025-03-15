using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public Text scoreText;
    public Text timerText;

    public void Start()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        timerText.text = "Timer: " + GameManager.Instance.timer;
    }
    
    public void RestartGame()
    {
        GameManager.Instance.FirstLevel();
    }
}