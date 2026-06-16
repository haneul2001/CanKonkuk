using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform player;
    public int mapSize = 7;

    [Header("난이도")]
    public Difficulty difficulty = Difficulty.Easy;

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    public Dictionary<Vector2Int, GameObject> tiles =
        new Dictionary<Vector2Int, GameObject>();

    public List<Vector2Int> activeTiles =
        new List<Vector2Int>();

    void Start()
    {
        if (mapSize % 2 == 0)
        {
            mapSize++;
        }

        CreateMap();
    }

    void CreateMap()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                CreateSquareTiles();
                break;

            case Difficulty.Normal:
                CreateCircleTiles();
                break;

            case Difficulty.Hard:
                CreateDiamondTiles();
                break;
        }
    }

    void CreateSquareTiles()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float x = -(mapSize / 2) + j;
                float y = (mapSize / 2) - i;

                CreateTile(x, y);
            }
        }
    }
void CreateCircleTiles()
{
    float radius = (mapSize - 2) / 2f;

    for (int i = 0; i < mapSize; i++)
    {
        for (int j = 0; j < mapSize; j++)
        {
            float x = -(mapSize / 2) + j;
            float y = (mapSize / 2) - i;

            if (x * x + y * y <= radius * radius)
            {
                CreateTile(x, y);
            }
        }
    }
}

    void CreateDiamondTiles()
    {
        int center = mapSize / 2;

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float x = -(mapSize / 2) + j;
                float y = (mapSize / 2) - i;

                if (Mathf.Abs(x) + Mathf.Abs(y) <= center)
                {
                    CreateTile(x, y);
                }
            }
        }
    }

    void CreateTile(float x, float y)
    {
        Vector2 pos = new Vector2(x, y);

        GameObject tile =
            Instantiate(tilePrefab, pos, Quaternion.identity);

        Vector2Int key = new Vector2Int((int)x, (int)y);

        tiles[key] = tile;
        tile.name = $"{key.x},{key.y}";

        activeTiles.Add(key);
    }

    public void DisableTile()
    {
        if (activeTiles.Count == 0) return;

        List<Vector2Int> candidates =
            new List<Vector2Int>(activeTiles);

        Vector2Int selectedPos =
            candidates[Random.Range(0, candidates.Count)];

        activeTiles.Remove(selectedPos);

        StartCoroutine(RemoveTileCoroutine(selectedPos));
    }

    public bool IsTileActive(Vector2Int pos)
    {
        if (!tiles.ContainsKey(pos)) return false;

        return tiles[pos].activeSelf;
    }

    IEnumerator RemoveTileCoroutine(Vector2Int pos)
    {
        GameObject tile = tiles[pos];
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

        sr.color = Color.red;

        float duration = 1f;
        float elapsed = 0f;

        Color startColor = sr.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, elapsed / duration);

            sr.color = c;

            yield return null;
        }

        Vector2Int playerPos =
            Vector2Int.RoundToInt(player.position);

        if (playerPos == pos)
        {
            ScoreManager.instance.GameOver();
            yield break;
        }

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                tile.transform.position,
                0.1f
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Item"))
            {
                Destroy(hit.gameObject);
            }
        }

        tile.SetActive(false);
    }
}