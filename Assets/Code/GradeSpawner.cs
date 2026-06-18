using System.Collections;
using UnityEngine;

public class GradeSpawner : MonoBehaviour
{
    public GameObject[] gradePrefabs;
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
        if (gradePrefabs == null || gradePrefabs.Length == 0) return;

        if (tileManager == null) return;

        int minLine = -(tileManager.mapSize / 2);

        int maxLine = minLine + tileManager.mapSize;
        int halfSize = tileManager.mapSize / 2;
        int side = Random.Range(0, 4);

        int gridLine = Random.Range(minLine, maxLine);

        float dynamicOffset = (tileManager.mapSize / 2) + 3f;

        dynamicOffset = halfSize + 3f;

        Vector2 spawnPos = Vector2.zero;
        Vector2 moveDir = Vector2.zero;

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