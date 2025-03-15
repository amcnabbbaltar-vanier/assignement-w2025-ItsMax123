using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int score = 0;
    public int timer = 0;
    private bool timerIsRunning = false;
    
    public event Action<int> OnScoreChange;
    public event Action<int> OnTimerChange;
    public event Action OnSwitchLevel;
    
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

    public void AddScore(int points)
    {
        score += points;
        OnScoreChange?.Invoke(score);
    }

    private IEnumerator Timer()
    {
        timerIsRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!timerIsRunning) break;
            timer++;
            OnTimerChange?.Invoke(timer);
        }
    }

    public void MainMenu()
    {
        timerIsRunning = false;
        SceneManager.LoadScene("MainMenu");
        OnSwitchLevel?.Invoke();
    }

    public void FirstLevel()
    {
        score = 0;
        timer = 0;
        StartCoroutine(Timer());
        SceneManager.LoadScene(1);
        OnSwitchLevel?.Invoke();
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnSwitchLevel?.Invoke();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        OnSwitchLevel?.Invoke();
    }

    public void EndScreen()
    {
        timerIsRunning = false;
        SceneManager.LoadScene("EndScreen");
        OnSwitchLevel?.Invoke();
    }
}
