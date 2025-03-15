using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    
    private Vector3 startPosition;

    public ParticleSystem doubleJumpParticles;
    public ParticleSystem pickUpParticles;

    public Slider healthBar;
    public Text scoreText;
    public Text timerText;

    public int health = 3;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        startPosition = transform.position;
        healthBar.maxValue = health;
        healthBar.value = health;
        ScoreChange(GameManager.Instance.score);
        TimerChange(GameManager.Instance.timer);
        CharacterMovement.OnDoubleJump += DoubleJump;
        GameManager.Instance.OnScoreChange += ScoreChange;
        GameManager.Instance.OnTimerChange += TimerChange;
        GameManager.Instance.OnSwitchLevel += RemoveSpeedBoost;
        GameManager.Instance.OnSwitchLevel += RemoveJumpBoost;
    }

    void OnDestroy()
    {
        CharacterMovement.OnDoubleJump -= DoubleJump;
        GameManager.Instance.OnScoreChange -= ScoreChange;
        GameManager.Instance.OnTimerChange -= TimerChange;
        GameManager.Instance.OnSwitchLevel -= RemoveSpeedBoost;
        GameManager.Instance.OnSwitchLevel -= RemoveJumpBoost;
    }

    private void OnTriggerEnter(Collider colliding)
    {
        switch (colliding.gameObject.tag)
        {
            case "PowerUp":
                pickUpParticles.Play();
                Destroy(colliding.gameObject);
                switch (colliding.gameObject.name)
                {
                    case "Speed Boost":
                        characterMovement.speedMultiplier = 1.5f;
                        Invoke(nameof(RemoveSpeedBoost), 5);
                        break;
                    case "Jump Boost":
                        characterMovement.canDoubleJump = true;
                        Invoke(nameof(RemoveJumpBoost), 30f);
                        break;
                    case "Score":
                        GameManager.Instance.AddScore(50);
                        break;
                }
                break;
            case "DeathPlane":
                Damage(1);
                gameObject.transform.position = startPosition;
                break;
            case "Trap":
                Damage(1);
                break;
            case "Finish":
                GameManager.Instance.NextLevel();
                break;
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            GameManager.Instance.RestartLevel();
        }
    }

    private void RemoveSpeedBoost()
    {
        characterMovement.speedMultiplier = 1;
    }

    private void RemoveJumpBoost()
    {
        characterMovement.canDoubleJump = false;
    }

    private void DoubleJump()
    {
        doubleJumpParticles.Play();
    }

    private void ScoreChange(int score)
    {
        scoreText.text = score + " Score";
    }

    private void TimerChange(int timer)
    {
        timerText.text = timer + " Timer";
    }
}