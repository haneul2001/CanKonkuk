using UnityEngine;
using System.Collections;

public class BookSpawner : MonoBehaviour
{
    public GameObject bookPrefab;
    public TileManager tileManager;
    public GameManager gameManager;
    public float spawnInterval = 5f;
    
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
            yield return new WaitForSeconds(spawnInterval);
            SpawnBook();
        }
    }
    void SpawnBook()
    {
        if(tileManager==null) return;
        if(tileManager.activeTiles.Count == 0) return;

         Vector2Int tilePos =
            tileManager.activeTiles[
                Random.Range(0, tileManager.activeTiles.Count)
            ];
        Vector2 spawnPos= new Vector2(tilePos.x, tilePos.y);
         Debug.Log($"책 생성 : {spawnPos}");
        Instantiate(bookPrefab, spawnPos,Quaternion.identity); 
    }
}
