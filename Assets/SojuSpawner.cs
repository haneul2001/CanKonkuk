using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SojuSpawner : MonoBehaviour
{
    public GameObject sojuPrefab;
    public TileManager tileManager;
    public GameManager gameManager;
    public float spawnInterval = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());    
    }

    IEnumerator SpawnRoutine()
    {
        if (!gameManager.GameStarted)
        {
            yield return null;
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnSoju();
        }
    }
    void SpawnSoju()
    {
        if(tileManager == null) return;
        if(tileManager.activeTiles.Count == 0) return;
        
        List<Vector2Int> candidates = new List<Vector2Int>();

        foreach(Vector2Int tilePos in tileManager.activeTiles)
        {
            Vector2 pos = new Vector2(tilePos.x, tilePos.y);
            Collider2D[] hits = Physics2D.OverlapCircleAll(pos,0.1f);
            bool canSpawn = true;
            foreach(Collider2D hit in hits)
            {
                if(hit.GetComponent<Book>() != null || hit.GetComponent<Soju>() != null||hit.CompareTag("Player"))
                {
                    canSpawn = false;
                    break;
                }
                
            }
            if (canSpawn)
                {
                    candidates.Add(tilePos);
                }
        }
        if(candidates.Count ==0)return;
        Vector2Int selectedTile = candidates[Random.Range(0, candidates.Count)];
        Vector2 spawnPos = new Vector2(selectedTile.x, selectedTile.y);

        Instantiate(sojuPrefab, spawnPos, Quaternion.identity);

    }
    void Update()
    {
        
    }
}
