using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("게임 시간 조절")]
    public float MaxTime => maxTime;
    public float TimeLimit => timeLimit;
    float maxTime = 100f;
    float timeLimit;

    public TileManager tileManager;
    void Start()
    {
        timeLimit = maxTime;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tileManager.DisableTile();
        }

        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0f)
        {
            timeLimit = 0f;
        }
    }
}
