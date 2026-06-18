using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        audioSource.PlayOneShot(clickSound);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(sceneName);
    }

    public void LoadTitle()
    {
        LoadScene("TITLE");
    }

    public void LoadStageSelect()
    {
        LoadScene("StageSelect");
    }

    public void LoadEasy()
    {
        LoadScene("Easy");
    }

    public void LoadNormal()
    {
        LoadScene("Normal");
    }

    public void LoadHard()
    {
        LoadScene("Hard");
    }

    public void LoadGameOver()
    {
        LoadScene("GameOver");
    }

    public void LoadGameClear()
    {
        LoadScene("GameClear");
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(clickSound);
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
    }
}