using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("점수 UI")]
    public Text gpaText;
    public Text CurrentScoreText;

    [Header("게임 오버")]
    public Image fadeImage;

    public int maxScore = 132;//132 학점
    public int currentScore = 0;
    public float totalScore = 0f; 
    public int gradeCount = 0;

    bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        gpaText.text = "평균 학점 : 0.00";

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }

        
    }

    public void AddGrade(float score)
    {
        if (score == 0f)
        {
            GameOver();
            return;
        }

        totalScore += score;
        gradeCount++;
        currentScore += 3;//한 과목 당 3학점

        float averageGPA = totalScore / gradeCount;

        gpaText.text = "평균 학점 : " + averageGPA.ToString("F2");
        CurrentScoreText.text = "현재 학점 : " + currentScore.ToString() + " / " + maxScore.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0f, 1f, elapsed / duration);

            fadeImage.color = c;

            yield return null;
        }

        
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("GameOver");
    }
    void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
}