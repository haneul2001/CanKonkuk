using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("게임 시간 조절")]
    public float MaxTime => maxTime;
    public float TimeLimit => timeLimit;

    [SerializeField] float maxTime = 60f;
    float timeLimit;

    public TileManager tileManager;
    public ScoreManager scoreManager;
    bool isGameClear = false;
    [Header("타일 제거 변수")]
    int currentRemovedCount = 0;
    int totalTilesToRemove;

    // [Header("게임 클리어 시스템")]
    // public float resultGrade = 0f;
    // public float clearTime = 0f;
    // public bool isCleare=false;
    // public float maxGrade = 132f;
    // public float currentGrade = 0f;
    [Header("게임 오버")]
    public Image fadeImage;
    bool isGameOver = false;

    Text bookCountText;

    public Text countdownText;
    bool gameStarted = false;
    public Player player;
    public bool GameStarted => gameStarted;
    void Start()
    {
        GameOverReason.hitF = false;
        Time.timeScale = 1f;
        timeLimit = maxTime;

        StartCoroutine(StartCountdown());
    }
    void Update()
    {
        
        if(!gameStarted)return;

        if(!isGameClear&& scoreManager.currentScore >= scoreManager.maxScore)
        {
            if(scoreManager.averageGPA >= 4.0f)
            {
                Debug.Log("게임 클리어");
                GameClear();
            }
            else
            {
                GameOverReason.hitF = false;
                Debug.Log("학점 부족");
                GameOver();
            }
            return;
        }

        if(Input.GetKeyDown(KeyCode.R)) ReloadScene();
        
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0f)
        {
            timeLimit = 0f;
        }

        float progress = 1f - (timeLimit / maxTime);

        int targetRemovedCount =
            Mathf.FloorToInt(
                totalTilesToRemove * progress * progress
            );

        while (currentRemovedCount < targetRemovedCount)
        {
            tileManager.DisableTile();
            currentRemovedCount++;
        }
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

        

        
        yield return new WaitForSecondsRealtime(1f);
    
        SceneManager.LoadScene("GameOver");
    }
    void GameClear()
    {
        if(isGameClear) return;
        isGameClear = true;
        StartCoroutine(GameClearRoutine());
    }
   
IEnumerator StartCountdown()
{
    yield return null; // TileManager.Start() 끝날 때까지 대기

    totalTilesToRemove =
        tileManager.activeTiles.Count - 1;

    Debug.Log($"activeTiles : {tileManager.activeTiles.Count}");
    Debug.Log($"totalTilesToRemove : {totalTilesToRemove}");

    countdownText.gameObject.SetActive(true);

    countdownText.text = "3";
    yield return new WaitForSeconds(1f);

    countdownText.text = "2";
    yield return new WaitForSeconds(1f);

    countdownText.text = "1";
    yield return new WaitForSeconds(1f);

    countdownText.text = "시작!";
    yield return new WaitForSeconds(0.5f);

    countdownText.gameObject.SetActive(false);

    gameStarted = true;
    player.canMove = true;
}    IEnumerator GameClearRoutine()
    {
        float duration = 1f;
        float elapsed =0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            fadeImage.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameClear");
    }

}