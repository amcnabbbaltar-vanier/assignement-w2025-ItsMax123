using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.FirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}