using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void LoadTitle()
    {
        SceneManager.LoadScene("TITLE");
    }

    public void LoadStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void LoadEasy()
    {
        SceneManager.LoadScene("Easy");
    }

    public void LoadNormal()
    {
        SceneManager.LoadScene("Normal");
    }

    public void LoadHard()
    {
        SceneManager.LoadScene("Hard");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}