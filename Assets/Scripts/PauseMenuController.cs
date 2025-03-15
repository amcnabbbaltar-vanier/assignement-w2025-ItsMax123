using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;
    
    [SerializeField] private GameObject pauseMenuPanel;
   
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }

    public void Restart()
    {
        GameManager.Instance.RestartLevel();
        ResumeGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.MainMenu();
        ResumeGame();
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
