using System.Collections;
using UnityEngine;

public class GradeSpawner : MonoBehaviour
{
    public GameObject[] gradePrefabs;
    public float spawnInterval = 1.0f;

    public TileManager tileManager;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnGrade();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGrade()
    {
        if (gradePrefabs == null || gradePrefabs.Length == 0) return;

        // 에러 방지: 타일 매니저가 연결되지 않았다면 작동하지 않음
        if (tileManager == null) return;

        // 1. 타일이 시작되는 가장 첫 번째 줄(min)을 구합니다.
        int minLine = -(tileManager.mapSize / 2);

        // 2. Random.Range는 마지막 숫자를 빼고 뽑으므로, 시작줄에 전체 맵 크기를 그대로 더해줍니다.
        int maxLine = minLine + tileManager.mapSize;
        int halfSize = tileManager.mapSize / 2;
        int side = Random.Range(0, 4);

        // 3. 정확하게 타일 개수만큼만 줄을 뽑아냅니다!
        int gridLine = Random.Range(minLine, maxLine);

        // 화면 밖 스폰 거리는 기존과 동일하게 유지해도 좋습니다. (원한다면 mapSize로 계산 가능)
        float dynamicOffset = (tileManager.mapSize / 2) + 3f;

        // 4. 맵 크기가 커지면 스폰 위치도 멀어지도록 자동 계산 (+3f는 화면 밖 여유 공간)
        dynamicOffset = halfSize + 3f;

        Vector2 spawnPos = Vector2.zero;
        Vector2 moveDir = Vector2.zero;

        // 5. 고정값 대신 dynamicOffset을 사용하도록 수정
        switch (side)
        {
            case 0:
                spawnPos = new Vector2(gridLine, dynamicOffset);
                moveDir = Vector2.down;
                break;
            case 1:
                spawnPos = new Vector2(gridLine, -dynamicOffset);
                moveDir = Vector2.up;
                break;
            case 2:
                spawnPos = new Vector2(-dynamicOffset, gridLine);
                moveDir = Vector2.right;
                break;
            case 3:
                spawnPos = new Vector2(dynamicOffset, gridLine);
                moveDir = Vector2.left;
                break;
        }

        int randomIndex = Random.Range(0, gradePrefabs.Length);
        GameObject selectedPrefab = gradePrefabs[randomIndex];

        GameObject newGrade = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        if (newGrade.TryGetComponent(out Grade gradeScript))
        {
            gradeScript.direction = moveDir;
        }
    }
}