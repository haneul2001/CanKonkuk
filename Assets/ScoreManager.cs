using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI gpaText;

    private float totalScore = 0f;      // 획득 점수의 합
    private int gradeCount = 0;         // 획득 성적 개수

    void Awake()
    {
        if (instance == null) instance = this;      // 싱글톤 패턴
    }

    void Start()
    {
        gpaText.text = "Score : 0.00";      // 시작 시 점수 0점으로 표시
    }
    public void AddGrade(float score)       // 성적 획득 시
    {
        if (score == 0f)
        {
            GameOver();         // 닿은 성적이 F학점(0점)이면 게임오버
            return;
        }

        totalScore += score;    // 점수 합산
        gradeCount++;           // 획득한 성적 개수 추가

        float averageGPA = totalScore / gradeCount;                 // 평균 점수 실시간 계산

        gpaText.text = "Score : " + averageGPA.ToString("F2");      // 평균 점수 UI 반영
    }

    void GameOver()
    {
        Time.timeScale = 0f; // F학점을 받아 게임 오버(게임 중지)
    }
}