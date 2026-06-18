using System.Collections;
using UnityEngine;

// 1. [오브젝트 + 확률]을 묶어주는 데이터 클래스 (구조 단순화)
[System.Serializable]
public class GradeSpawnData
{
    public string gradeName = "A+";
    public GameObject gradePrefab;
    [Range(0f, 1f)] public float probability = 0.2f;
}

public class GradeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    // 🌟 복잡한 난이도 배열을 없애고, 바로 리스트를 노출합니다.
    public GradeSpawnData[] gradeList;

    [Header("Spawner Settings")]
    public float spawnInterval = 1.0f;
    public GameManager gameManager;
    public TileManager tileManager;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (!gameManager.GameStarted)
        {
            yield return null;
        }
        while (true)
        {
            SpawnGrade();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGrade()
    {
        // 리스트가 비어있으면 실행하지 않음
        if (gradeList == null || gradeList.Length == 0) return;
        if (tileManager == null) return;

        int minLine = -(tileManager.mapSize / 2);
        int maxLine = minLine + tileManager.mapSize;
        int halfSize = tileManager.mapSize / 2;
        int side = Random.Range(0, 4);
        int gridLine = Random.Range(minLine, maxLine);
        float dynamicOffset = halfSize + 3f;

        Vector2 spawnPos = Vector2.zero;
        Vector2 moveDir = Vector2.zero;

        switch (side)
        {
            case 0: spawnPos = new Vector2(gridLine, dynamicOffset); moveDir = Vector2.down; break;
            case 1: spawnPos = new Vector2(gridLine, -dynamicOffset); moveDir = Vector2.up; break;
            case 2: spawnPos = new Vector2(-dynamicOffset, gridLine); moveDir = Vector2.right; break;
            case 3: spawnPos = new Vector2(dynamicOffset, gridLine); moveDir = Vector2.left; break;
        }

        GameObject selectedPrefab = GetGradePrefabByProbability();
        if (selectedPrefab == null) return;

        GameObject newGrade = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        if (newGrade.TryGetComponent(out Grade gradeScript))
        {
            gradeScript.direction = moveDir;
        }
    }

    // 🌟 현재 씬에 설정된 등급 리스트에서 확률에 따라 뽑아주는 함수
    GameObject GetGradePrefabByProbability()
    {
        float totalProb = 0f;
        foreach (var grade in gradeList)
        {
            totalProb += grade.probability;
        }

        float randomValue = Random.Range(0f, totalProb);
        float cumulativeProb = 0f;

        foreach (var grade in gradeList)
        {
            cumulativeProb += grade.probability;
            if (randomValue <= cumulativeProb)
            {
                return grade.gradePrefab;
            }
        }

        return gradeList[0].gradePrefab;
    }
}